using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using P3AddNewFunctionalityDotNetCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace P3AddNewFunctionalityDotNetCore.IntegrationTests
{
    public abstract class DBTestBaseController
    {
        public DbContextOptions<P3Referential> options;
        public P3Referential context;

        public DBTestBaseController()
        {
            options = new DbContextOptionsBuilder<P3Referential>().UseSqlServer("Server=.\\SQLEXPRESS;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            context = new P3Referential(options);

        }
        

    }
}
