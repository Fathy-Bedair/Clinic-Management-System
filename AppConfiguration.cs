namespace Clinic_Management_System
{
    public static class AppConfiguration
    {
        public static void RegisterConfig(this IServiceCollection services,ConfigurationManager configuration)
        {
            var connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string"
            + "'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IRepository<Specialization>, Repository<Specialization>>();
            services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();
            services.AddScoped<IRepository<Appointment>, Repository<Appointment>>();
            services.AddScoped<IRepository<Patient>, Repository<Patient>>();

        }
    }
}
