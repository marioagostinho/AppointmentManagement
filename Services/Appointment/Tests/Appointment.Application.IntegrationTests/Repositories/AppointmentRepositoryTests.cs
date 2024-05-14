using Entities = Appointment.Domain.Entities;
using Appointment.Domain.Repositories;
using Appointment.Persistence.DatabaseContext;
using Appointment.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Appointment.Application.IntegrationTests.Repositories
{
    public class AppointmentRepositoryTests
    {
        private readonly DbContextOptions<AppointmentDatabaseContext> _options;
        private readonly AppointmentDatabaseContext _context;
        private readonly IAppointmentRepository _applicationRepository;

        public AppointmentRepositoryTests()
        {
            // Options
            _options = new DbContextOptionsBuilder<AppointmentDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "AppointmentDbTest")
                .Options;

            // Context
            _context = new AppointmentDatabaseContext(_options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var appointments = new List<Entities.Appointment>
            {
                new Entities.Appointment
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = DateTime.Now,
                    Duration = TimeSpan.FromHours(8),
                    TeamId = Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
                },
                new Entities.Appointment
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = DateTime.Now,
                    Duration = TimeSpan.FromHours(8),
                    TeamId = Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
                },
                new Entities.Appointment
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = DateTime.Now,
                    Duration = TimeSpan.FromHours(8),
                    TeamId = Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
                }
            };

            _context.Appointments.AddRange(appointments);
            _context.SaveChanges();

            // Application repository
            _applicationRepository = new AppointmentRepository(_context);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsAppointment_WhenIdExists()
        {
            // Arrange
            var expectedAppointment = _context.Appointments.First();

            // Act
            var result = await _applicationRepository.GetByIdAsync(expectedAppointment.Id);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeEquivalentTo(expectedAppointment);
            result.ShouldBeOfType<Entities.Appointment>();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenIdsDoesNotExists()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _applicationRepository.GetByIdAsync(id);

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetByIdRangeAsync_ReturnsAppointments_WhenIdsExists()
        {
            // Arrange
            var expectedAppointments = _context.Appointments.ToList();
            var expectedAppointmentIds = new HashSet<Guid>(expectedAppointments
                .Select(p => p.Id)
                .ToList());

            // Act
            var result = await _applicationRepository.GetByIdRangeAsync(expectedAppointmentIds);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeEquivalentTo(expectedAppointments);
            result.Count.ShouldBe(expectedAppointments.Count());
            result.ShouldBeOfType<List<Entities.Appointment>>();
        }

        [Fact]
        public async Task GetByIdRangeAsync_ReturnsEmptyArray_WhenIdsDontExists()
        {
            // Arrange
            var ids = new HashSet<Guid>();
            ids.Add(Guid.NewGuid());
            ids.Add(Guid.NewGuid());
            ids.Add(Guid.NewGuid());

            // Act
            var result = await _applicationRepository.GetByIdRangeAsync(ids);

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
            result.ShouldBeOfType<List<Entities.Appointment>>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllAppointments()
        {
            // Arrange
            var expectedAppointments = _context.Appointments.ToList();

            // Act
            var result = await _applicationRepository.GetAllAsync();

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeEquivalentTo(expectedAppointments);
            result.Count.ShouldBe(expectedAppointments.Count());
            result.ShouldBeOfType<List<Entities.Appointment>>();
        }
    }
}
