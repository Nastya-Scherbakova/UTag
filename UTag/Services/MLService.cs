using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UTag.Helpers;
using UTag.Models;
using UTag.Services.Interfaces;
using UTag.ViewModels;

namespace UTag.Services
{
    public class MLService : IMLService
    {
        private readonly UTagContext _context;
        private MLContext mlContext;
        private ITransformer model;

        public MLService(UTagContext context)
        {
            _context = context;
            InitML();
        }

        public async Task<IEnumerable<Product>> GetReccomendedProducts(Person person)
        {
            var prediction = model.CreatePredictionEngine<MLPerson, List<float>>(mlContext).Predict(MLPerson.ToMLPerson(person));
            return new List<Product>();
        }

        public async Task<IEnumerable<Person>> GetPersons(Person person)
        {
            var persons = await _context.Persons
                .Include(el=>el.LikedProducts)
                .Include(el=>el.ConnectedTags)
                .Where(el => el.BirthDate.Year >= person.BirthDate.Year - 5 && el.BirthDate.Year <= person.BirthDate.Year + 5)
                .OrderByDescending(el=>compareTags(el.ConnectedTags, person.ConnectedTags))
                .Take(10)
                .ToListAsync();
            if(persons.IndexOf(person) == -1) persons.Add(person);
            return persons;
        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            var persons = await _context.Persons
                .Include(el => el.LikedProducts)
                .Include(el => el.ConnectedTags)
                .Take(1000)
                .ToListAsync();
            return persons;
        }

        private int compareTags(ICollection<PersonTag> first, ICollection<PersonTag> second)
        {
            var compare = 0;
            foreach(var tag in first)
            {
                compare+= second.Any(el => el.TagId == tag.TagId) ? 1 : 0;
            }
            return compare;
        }

        public async void InitML()
        {
            mlContext = new MLContext();
            IEnumerable<Person> churnData = await GetAllPersons();
            var trainData = mlContext.CreateStreamingDataView(MLPerson.AllToMLPerson(churnData));
            var pipeline = mlContext.Transforms.CopyColumns("LikedProducts", "Label")
                .Append(mlContext.Transforms.Concatenate("Features", "LikedProducts", "Tags", "Age"))
                .Append(mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(new string[] {"Features"}));

            model = pipeline.Fit(trainData);
            SaveModelAsFile(mlContext, model);
        }

        private static void SaveModelAsFile(MLContext mlContext, ITransformer model)
        {
            var _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");
            using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
                mlContext.Model.Save(model, fileStream);
        }
        
        
    }

    public class MLPerson
    {
        public float Id { get; set; }
        public float Age { get; set; }
        [VectorType]
        public float[] LikedProducts { get; set; }
        [VectorType]
        public float[] Tags { get; set; }

        public static MLPerson ToMLPerson(Person person)
        {
            var ml = new MLPerson();
            ml.Id = person.Id;
            ml.Age = DateTime.Now.Year - person.BirthDate.Year;
            if (DateTime.Now.DayOfYear < person.BirthDate.DayOfYear)
                ml.Age = ml.Age - 1;
            ml.Tags = new float[person.ConnectedTags.Count];
            for(var i =0; i< person.ConnectedTags.Count; i++)
            {
                ml.Tags[i] = person.ConnectedTags.ElementAt(i).TagId;
            }
            ml.LikedProducts = new float[person.LikedProducts.Count];
            for (var i = 0; i < person.LikedProducts.Count; i++)
            {
                ml.LikedProducts[i] = person.LikedProducts.ElementAt(i).ProductId;
            }
            return ml;
        }

        public static IEnumerable<MLPerson> AllToMLPerson(IEnumerable<Person> persons)
        {
            return persons.Select(ToMLPerson).ToList();
        }
    }
}
