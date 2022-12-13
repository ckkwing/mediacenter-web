using Microsoft.AspNetCore.Identity;
using Web.Service.DataAccess.Entity;
using Web.Service.DataAccess;
using Web.Service.Model;
using Utilities.Extensions;

namespace Web.Service.Data
{
    public class BaseDbInitializer
    {
        public static void Initialize(DBContext context)
        {
            context.Database.EnsureCreated();
            if (context.Videos.Any())
                return; //DB has been seeded

            for (int i =0; i<1000; i++)
            {
                var video = new Video()
                {
                    Id = Guid.NewGuid(),
                    Name = $"FH3_540p60_{i+1}",
                    Description = $"A test video {i+1}",
                    Suffix = Suffix.AVI.GetDescription(),
                    Size = 97663038,
                };

                context.Videos.Add(video);
            }
            //var video = new Video() 
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "FH3_540p60",
            //    Description = "A test video",
            //    Suffix = Suffix.AVI.GetDescription(),
            //    Size = 97663038,
            //};

            //context.Videos.Add(video);
            int iRel = context.SaveChanges();
        }
    }
}
