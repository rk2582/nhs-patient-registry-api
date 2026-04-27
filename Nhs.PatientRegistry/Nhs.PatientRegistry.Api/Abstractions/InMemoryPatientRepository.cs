using Nhs.PatientRegistry.Api.Models;

namespace Nhs.PatientRegistry.Api.Abstractions
{
    public class InMemoryPatientRepository :IPatientRepository
    {
        private static readonly Patient[] Patients =
                {
                    new Patient
                    {
                        Id = 1,
                        NHSNumber = "1111111111",
                        Name = "Lionel Messi",
                        DateOfBirth = new DateTime(1985, 4, 12),
                        GPPractice = "Delapre Medical Centre Northampton"
                    },
                    new Patient
                    {
                        Id = 2,
                        NHSNumber = "2222222222",
                        Name = "Cristiano Ronaldo",
                        DateOfBirth = new DateTime(1992, 8, 25),
                        GPPractice = "Delapre Medical Centre Northampton"
                    },
                    new Patient
                    {
                        Id = 3,
                        NHSNumber = "3333333333",
                        Name = "Kylien Mbappe",
                        DateOfBirth = new DateTime(1978, 11, 3),
                        GPPractice = "Delapre Medical Centre Northampton"
                    },
                    new Patient
                    {
                        Id = 4,
                        NHSNumber = "4444444444",
                        Name = "Harry Kane",
                        DateOfBirth = new DateTime(2001, 2, 18),
                        GPPractice = "Delapre Medical Centre Northampton"
                    }
                };
        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            return await Task.FromResult(Patients.AsEnumerable());
        }
    }
}
