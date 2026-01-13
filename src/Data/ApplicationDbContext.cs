using Microsoft.EntityFrameworkCore;

namespace contosohealth.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medication> Medications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Patient Data
        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(1985, 3, 15),
                Gender = "Male",
                PhoneNumber = "555-0101",
                Email = "john.smith@email.com",
                Address = "123 Oak Street, Seattle, WA 98101",
                BloodType = "A+",
                Allergies = "Penicillin, Peanuts",
                MedicalHistory = "Hypertension, Type 2 Diabetes",
                PhotoUrl = "/images/patients/patient1.jpg",
                RegistrationDate = new DateTime(2020, 1, 10)
            },
            new Patient
            {
                Id = 2,
                FirstName = "Emily",
                LastName = "Johnson",
                DateOfBirth = new DateTime(1992, 7, 22),
                Gender = "Female",
                PhoneNumber = "555-0102",
                Email = "emily.j@email.com",
                Address = "456 Pine Avenue, Seattle, WA 98102",
                BloodType = "O-",
                Allergies = "None",
                MedicalHistory = "Asthma",
                PhotoUrl = "/images/patients/patient2.jpg",
                RegistrationDate = new DateTime(2019, 5, 15)
            },
            new Patient
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Williams",
                DateOfBirth = new DateTime(1978, 11, 8),
                Gender = "Male",
                PhoneNumber = "555-0103",
                Email = "m.williams@email.com",
                Address = "789 Maple Drive, Seattle, WA 98103",
                BloodType = "B+",
                Allergies = "Latex, Shellfish",
                MedicalHistory = "Heart Disease, High Cholesterol",
                PhotoUrl = "/images/patients/patient3.jpg",
                RegistrationDate = new DateTime(2018, 8, 20)
            },
            new Patient
            {
                Id = 4,
                FirstName = "Sarah",
                LastName = "Brown",
                DateOfBirth = new DateTime(1995, 2, 14),
                Gender = "Female",
                PhoneNumber = "555-0104",
                Email = "sarah.brown@email.com",
                Address = "321 Elm Street, Seattle, WA 98104",
                BloodType = "AB+",
                Allergies = "None",
                MedicalHistory = "Seasonal Allergies",
                PhotoUrl = "/images/patients/patient4.jpg",
                RegistrationDate = new DateTime(2021, 3, 5)
            },
            new Patient
            {
                Id = 5,
                FirstName = "David",
                LastName = "Martinez",
                DateOfBirth = new DateTime(1988, 9, 30),
                Gender = "Male",
                PhoneNumber = "555-0105",
                Email = "d.martinez@email.com",
                Address = "654 Cedar Lane, Seattle, WA 98105",
                BloodType = "O+",
                Allergies = "Sulfa drugs",
                MedicalHistory = "Kidney Stones",
                PhotoUrl = "/images/patients/patient5.jpg",
                RegistrationDate = new DateTime(2022, 6, 12)
            },
            new Patient
            {
                Id = 6,
                FirstName = "Jennifer",
                LastName = "Garcia",
                DateOfBirth = new DateTime(1990, 5, 18),
                Gender = "Female",
                PhoneNumber = "555-0106",
                Email = "jen.garcia@email.com",
                Address = "987 Birch Road, Seattle, WA 98106",
                BloodType = "A-",
                Allergies = "Aspirin",
                MedicalHistory = "Migraines",
                PhotoUrl = "/images/patients/patient6.jpg",
                RegistrationDate = new DateTime(2020, 11, 28)
            },
            new Patient
            {
                Id = 7,
                FirstName = "Robert",
                LastName = "Anderson",
                DateOfBirth = new DateTime(1982, 12, 5),
                Gender = "Male",
                PhoneNumber = "555-0107",
                Email = "rob.anderson@email.com",
                Address = "147 Spruce Court, Seattle, WA 98107",
                BloodType = "B-",
                Allergies = "Iodine",
                MedicalHistory = "Thyroid Disorder",
                PhotoUrl = "/images/patients/patient7.jpg",
                RegistrationDate = new DateTime(2019, 2, 17)
            },
            new Patient
            {
                Id = 8,
                FirstName = "Lisa",
                LastName = "Thompson",
                DateOfBirth = new DateTime(1987, 4, 25),
                Gender = "Female",
                PhoneNumber = "555-0108",
                Email = "lisa.t@email.com",
                Address = "258 Willow Way, Seattle, WA 98108",
                BloodType = "O+",
                Allergies = "Bee stings",
                MedicalHistory = "None",
                PhotoUrl = "/images/patients/patient8.jpg",
                RegistrationDate = new DateTime(2023, 1, 9)
            }
        );

        // Seed Doctor Data
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            {
                Id = 1,
                FirstName = "Dr. Margaret",
                LastName = "Chen",
                Specialization = "Cardiology",
                LicenseNumber = "MD-123456",
                PhoneNumber = "555-0201",
                Email = "m.chen@hospital.com",
                Department = "Cardiology",
                YearsOfExperience = 15,
                Education = "Harvard Medical School, MD",
                Bio = "Board-certified cardiologist specializing in interventional cardiology and heart disease prevention.",
                PhotoUrl = "/images/doctors/doctor1.jpg",
                JoinDate = new DateTime(2015, 6, 1)
            },
            new Doctor
            {
                Id = 2,
                FirstName = "Dr. James",
                LastName = "Wilson",
                Specialization = "Orthopedic Surgery",
                LicenseNumber = "MD-234567",
                PhoneNumber = "555-0202",
                Email = "j.wilson@hospital.com",
                Department = "Orthopedics",
                YearsOfExperience = 20,
                Education = "Johns Hopkins University, MD",
                Bio = "Experienced orthopedic surgeon specializing in joint replacement and sports medicine.",
                PhotoUrl = "/images/doctors/doctor2.jpg",
                JoinDate = new DateTime(2012, 3, 15)
            },
            new Doctor
            {
                Id = 3,
                FirstName = "Dr. Priya",
                LastName = "Patel",
                Specialization = "Pediatrics",
                LicenseNumber = "MD-345678",
                PhoneNumber = "555-0203",
                Email = "p.patel@hospital.com",
                Department = "Pediatrics",
                YearsOfExperience = 12,
                Education = "Stanford University School of Medicine, MD",
                Bio = "Compassionate pediatrician dedicated to providing comprehensive care for children of all ages.",
                PhotoUrl = "/images/doctors/doctor3.jpg",
                JoinDate = new DateTime(2016, 9, 1)
            },
            new Doctor
            {
                Id = 4,
                FirstName = "Dr. Robert",
                LastName = "Taylor",
                Specialization = "Neurology",
                LicenseNumber = "MD-456789",
                PhoneNumber = "555-0204",
                Email = "r.taylor@hospital.com",
                Department = "Neurology",
                YearsOfExperience = 18,
                Education = "Mayo Clinic Alix School of Medicine, MD",
                Bio = "Neurologist specializing in stroke treatment and neurodegenerative diseases.",
                PhotoUrl = "/images/doctors/doctor4.jpg",
                JoinDate = new DateTime(2013, 1, 10)
            },
            new Doctor
            {
                Id = 5,
                FirstName = "Dr. Amanda",
                LastName = "Rodriguez",
                Specialization = "Dermatology",
                LicenseNumber = "MD-567890",
                PhoneNumber = "555-0205",
                Email = "a.rodriguez@hospital.com",
                Department = "Dermatology",
                YearsOfExperience = 10,
                Education = "UCLA David Geffen School of Medicine, MD",
                Bio = "Dermatologist specializing in medical and cosmetic dermatology, skin cancer treatment.",
                PhotoUrl = "/images/doctors/doctor5.jpg",
                JoinDate = new DateTime(2018, 7, 20)
            },
            new Doctor
            {
                Id = 6,
                FirstName = "Dr. Michael",
                LastName = "Kim",
                Specialization = "Internal Medicine",
                LicenseNumber = "MD-678901",
                PhoneNumber = "555-0206",
                Email = "m.kim@hospital.com",
                Department = "Internal Medicine",
                YearsOfExperience = 14,
                Education = "University of Pennsylvania Perelman School of Medicine, MD",
                Bio = "Internal medicine physician with expertise in chronic disease management and preventive care.",
                PhotoUrl = "/images/doctors/doctor6.jpg",
                JoinDate = new DateTime(2014, 11, 5)
            },
            new Doctor
            {
                Id = 7,
                FirstName = "Dr. Rachel",
                LastName = "Foster",
                Specialization = "Obstetrics and Gynecology",
                LicenseNumber = "MD-789012",
                PhoneNumber = "555-0207",
                Email = "r.foster@hospital.com",
                Department = "OB/GYN",
                YearsOfExperience = 16,
                Education = "Columbia University Vagelos College of Physicians and Surgeons, MD",
                Bio = "OB/GYN specializing in high-risk pregnancies and minimally invasive gynecologic surgery.",
                PhotoUrl = "/images/doctors/doctor7.jpg",
                JoinDate = new DateTime(2011, 4, 18)
            },
            new Doctor
            {
                Id = 8,
                FirstName = "Dr. Thomas",
                LastName = "Hughes",
                Specialization = "Emergency Medicine",
                LicenseNumber = "MD-890123",
                PhoneNumber = "555-0208",
                Email = "t.hughes@hospital.com",
                Department = "Emergency Department",
                YearsOfExperience = 11,
                Education = "Duke University School of Medicine, MD",
                Bio = "Emergency medicine physician with experience in trauma care and critical care medicine.",
                PhotoUrl = "/images/doctors/doctor8.jpg",
                JoinDate = new DateTime(2017, 2, 22)
            }
        );

        // Seed Medication Data
        modelBuilder.Entity<Medication>().HasData(
            new Medication
            {
                Id = 1,
                Name = "Amoxicillin 500mg",
                EAN = "8712345678901",
                CostPrice = 12.50m,
                Dose = 500,
                DoseUnit = "mg",
                Description = "Antibiotic used to treat bacterial infections",
                Manufacturer = "Pfizer",
                CreatedDate = new DateTime(2023, 1, 15)
            },
            new Medication
            {
                Id = 2,
                Name = "Ibuprofen 400mg",
                EAN = "8712345678902",
                CostPrice = 8.75m,
                Dose = 400,
                DoseUnit = "mg",
                Description = "Non-steroidal anti-inflammatory drug for pain relief",
                Manufacturer = "Johnson & Johnson",
                CreatedDate = new DateTime(2023, 1, 20)
            },
            new Medication
            {
                Id = 3,
                Name = "Metformin 850mg",
                EAN = "8712345678903",
                CostPrice = 15.30m,
                Dose = 850,
                DoseUnit = "mg",
                Description = "Medication for type 2 diabetes",
                Manufacturer = "Merck",
                CreatedDate = new DateTime(2023, 2, 5)
            },
            new Medication
            {
                Id = 4,
                Name = "Lisinopril 10mg",
                EAN = "8712345678904",
                CostPrice = 18.90m,
                Dose = 10,
                DoseUnit = "mg",
                Description = "ACE inhibitor for high blood pressure",
                Manufacturer = "AstraZeneca",
                CreatedDate = new DateTime(2023, 2, 12)
            },
            new Medication
            {
                Id = 5,
                Name = "Atorvastatin 20mg",
                EAN = "8712345678905",
                CostPrice = 22.40m,
                Dose = 20,
                DoseUnit = "mg",
                Description = "Statin medication to lower cholesterol",
                Manufacturer = "Pfizer",
                CreatedDate = new DateTime(2023, 3, 1)
            },
            new Medication
            {
                Id = 6,
                Name = "Omeprazole 20mg",
                EAN = "8712345678906",
                CostPrice = 10.25m,
                Dose = 20,
                DoseUnit = "mg",
                Description = "Proton pump inhibitor for acid reflux",
                Manufacturer = "AstraZeneca",
                CreatedDate = new DateTime(2023, 3, 10)
            },
            new Medication
            {
                Id = 7,
                Name = "Aspirin 100mg",
                EAN = "8712345678907",
                CostPrice = 5.50m,
                Dose = 100,
                DoseUnit = "mg",
                Description = "Pain reliever and blood thinner",
                Manufacturer = "Bayer",
                CreatedDate = new DateTime(2023, 3, 15)
            },
            new Medication
            {
                Id = 8,
                Name = "Levothyroxine 75mcg",
                EAN = "8712345678908",
                CostPrice = 14.80m,
                Dose = 0.075m,
                DoseUnit = "mg",
                Description = "Thyroid hormone replacement therapy",
                Manufacturer = "Abbott",
                CreatedDate = new DateTime(2023, 4, 1)
            },
            new Medication
            {
                Id = 9,
                Name = "Amlodipine 5mg",
                EAN = "8712345678909",
                CostPrice = 11.60m,
                Dose = 5,
                DoseUnit = "mg",
                Description = "Calcium channel blocker for hypertension",
                Manufacturer = "Norvasc",
                CreatedDate = new DateTime(2023, 4, 8)
            },
            new Medication
            {
                Id = 10,
                Name = "Vitamin D3",
                EAN = "8712345678910",
                CostPrice = 9.99m,
                Dose = 1,
                DoseUnit = "pill",
                Description = "Dietary supplement for bone health",
                Manufacturer = "Nature Made",
                CreatedDate = new DateTime(2023, 4, 15)
            }
        );
    }
}
