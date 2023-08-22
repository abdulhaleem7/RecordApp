using Microsoft.EntityFrameworkCore;
using RecordSetup.Entities;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace RecordSetup.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<SubRegionRecord> SubRegionRecords { get; set; }
        public DbSet<SubRegionRecordTable> SubRegionRecordTables { get; set; }
        public DbSet<TableSchema> TableSchemas { get; set; }
    }
}
