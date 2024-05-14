namespace Appointment.Application.UnitTests.Mocks
{
    public class MockAppointmentRepository
    {
        public static Mock<IAppointmentRepository> GetMockAppointmentRepository()
        {
            DateTime now = DateTime.Now;

            // Setup appointment data
            var appointments = new List<Entities.Appointment>
            {
                new Entities.Appointment
                {
                    Id = Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b7"),
                    StartDateTime = ConstructDate(now, 1, new TimeSpan(8,0,0)),
                    Duration = TimeSpan.FromMinutes(15),
                    TeamId = Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
                },
                new Entities.Appointment
                {
                    Id = Guid.Parse("ac3e602b-75ae-499d-9c75-29fbdfeb0f29"),
                    StartDateTime = ConstructDate(now, 1, new TimeSpan(8,15,0)),
                    Duration = TimeSpan.FromMinutes(15),
                    TeamId = Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
                },
                new Entities.Appointment
                {
                    Id = Guid.Parse("5e35d8ac-4178-473a-89b5-b99c1746fd7c"),
                    StartDateTime = ConstructDate(now, 1, new TimeSpan(8,30,0)),
                    Duration = TimeSpan.FromMinutes(15),
                    TeamId = Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
                },
                new Entities.Appointment
                {
                    Id = Guid.Parse("054e67b7-bb8f-4cd6-b9af-79047ea808ac"),
                    StartDateTime = ConstructDate(now, 1, new TimeSpan(8,30,0)),
                    Duration = TimeSpan.FromMinutes(15),
                    TeamId = Guid.Parse("b636bace-3635-40f3-a0ae-d2c6609a137d")
                },
                new Entities.Appointment
                {
                    Id = Guid.Parse("99b03cd9-35cf-489b-9d2e-9584d6f5e10b"),
                    StartDateTime = ConstructDate(now, 1, new TimeSpan(8,15,0)),
                    Duration = TimeSpan.FromMinutes(15),
                    TeamId = Guid.Parse("3f03f29d-3fb0-4622-b1df-01410af78366")
                }
            };

            var mock = new Mock<IAppointmentRepository>();

            // Setup for GetAllAsync
            mock.Setup(r => r.GetAllAsync(It.IsAny<bool>())).ReturnsAsync(appointments);

            // Setup for GetByIdAsync
            mock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                    .ReturnsAsync((Guid id, bool track) => appointments.FirstOrDefault(a => a.Id == id));

            // Setup for GetByTeamIdAndDateAsync
            mock.Setup(r => r.GetByTeamIdAndDateAsync(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                    .ReturnsAsync((Guid teamId, DateTime date) =>
                        appointments.Where(a => a.TeamId == teamId && 
                            a.StartDateTime.Year == date.Year &&
                            a.StartDateTime.Month == date.Month &&
                            a.StartDateTime.Day == date.Day).ToList());

            // Setup for CreateAsync
            mock.Setup(r => r.CreateAsync(It.IsAny<Entities.Appointment>()))
                    .Returns((Entities.Appointment appointment) =>
                    {
                        appointment.Id = Guid.NewGuid();
                        appointments.Add(appointment);
                        return Task.FromResult(appointment.Id);
                    });

            // Setup for UpdateAsync
            mock.Setup(r => r.UpdateAsync(It.IsAny<Entities.Appointment>()))
                    .ReturnsAsync((Entities.Appointment appointment) =>
                    {
                        var index = appointments.FindIndex(a => a.Id == appointment.Id);
                        if (index != -1)
                        {
                            appointments[index] = appointment;
                            return true;
                        }
                        return false;
                    });

            // Setup for DeleteAsync
            mock.Setup(r => r.DeleteAsync(It.IsAny<Entities.Appointment>()))
                    .ReturnsAsync((Entities.Appointment appointment) =>
                    {
                        return appointments.Remove(appointment);
                    });

            return mock;
        }

        public static DateTime ConstructDate(DateTime date, int extraDay, TimeSpan time)
        {
            var addedDay = date.AddDays(extraDay);

            return new DateTime(
                addedDay.Year,
                addedDay.Month,
                addedDay.Day,
                time.Hours,
                time.Minutes,
                time.Seconds
            );
        }
    }
}
