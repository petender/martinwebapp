# Contoso Clinic - Healthcare Management System

A modern healthcare management web application built with ASP.NET Core, featuring patient records management, doctor profiles, and secure authentication.

## 🏥 Overview

Contoso Clinic is a comprehensive healthcare management system designed to manage patient information and doctor profiles with secure access control. The application provides an intuitive interface for healthcare providers to access and manage patient data efficiently.

## 🏗️ Architecture

### Application Architecture
- **Pattern**: MVC (Model-View-Controller) with Razor Pages
- **Database Access**: Entity Framework Core with Code-First approach
- **Authentication**: Cookie-based authentication with role-based access control
- **Design Pattern**: Repository pattern through EF Core DbContext

### Technology Stack

#### Backend
- **Framework**: ASP.NET Core 9.0 (Razor Pages)
- **Language**: C# .NET 9.0
- **ORM**: Entity Framework Core 9.0.0
- **Database**: SQLite 3.x
- **Authentication**: ASP.NET Core Identity (Cookie Authentication)
- **Additional Libraries**:
  - Azure.AI.OpenAI 2.1.0
  - Microsoft.EntityFrameworkCore.Sqlite 9.0.0
  - Microsoft.EntityFrameworkCore.Design 9.0.0

#### Frontend
- **UI Framework**: Bootstrap 5.3
- **JavaScript**: Vanilla JavaScript (ES6+)
- **CSS**: Custom CSS with CSS3 Gradients and Animations
- **Icons**: Unicode Emojis
- **Responsive Design**: Mobile-first approach using Bootstrap grid system

#### Database
- **Database Engine**: SQLite
- **Database File**: `healthcare.db`
- **Schema Management**: EF Core Migrations with Database.EnsureCreated()
- **Data Seeding**: Built-in seed data for 8 patients and 8 doctors

## 📊 Data Models

### Patient Entity
- **Properties**: Id, FirstName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address, BloodType, Allergies, MedicalHistory, RegistrationDate, PhotoUrl
- **Features**: Complete medical profile with photo support

### Doctor Entity
- **Properties**: Id, FirstName, LastName, Specialization, LicenseNumber, PhoneNumber, Email, Department, YearsOfExperience, Education, Bio, JoinDate, PhotoUrl
- **Features**: Professional credentials with detailed bio and photo

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Visual Studio 2022, VS Code, or any C# IDE
- Git (for version control)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd martinwebapp
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the application**
   - Open your browser and navigate to: `http://localhost:5258`

### Database Setup

The database is automatically created on first run with seed data:
- **8 Pre-configured Patients** with complete medical records and photos
- **8 Pre-configured Doctors** with specializations and credentials
- SQLite database file: `healthcare.db` (auto-generated in project root)

## 🔐 Authentication & Security

### Security Features
- Cookie-based authentication with 8-hour session expiration
- Role-based access control (Doctor role)
- Protected patient data routes
- Secure password validation
- HTTPS redirection (configurable)

### Login Credentials

**Username Format**: First 2 letters of first name + First 6 letters of last name (no spaces)

**Password**: `Doctor123!` (for all doctors)

#### Available Doctor Accounts:

| Username    | Doctor Name           | Specialization            | Department          |
|-------------|----------------------|---------------------------|---------------------|
| `machen`    | Dr. Margaret Chen    | Cardiology                | Cardiology          |
| `jawilson`  | Dr. James Wilson     | Orthopedic Surgery        | Orthopedics         |
| `prpatel`   | Dr. Priya Patel      | Pediatrics                | Pediatrics          |
| `rotaylor`  | Dr. Robert Taylor    | Neurology                 | Neurology           |
| `amrodrig`  | Dr. Amanda Rodriguez | Dermatology               | Dermatology         |
| `mikim`     | Dr. Michael Kim      | Internal Medicine         | Internal Medicine   |
| `rafoster`  | Dr. Rachel Foster    | Obstetrics and Gynecology | OB/GYN              |
| `thhughes`  | Dr. Thomas Hughes    | Emergency Medicine        | Emergency Department|

**Example Login**:
- Username: `machen`
- Password: `Doctor123!`

## 📁 Project Structure

```
martinwebapp/
├── Data/
│   ├── ApplicationDbContext.cs    # EF Core DbContext with seed data
│   ├── Patient.cs                  # Patient entity model
│   ├── Doctor.cs                   # Doctor entity model
│   └── Employee.cs                 # Legacy employee model
├── Pages/
│   ├── Index.cshtml                # Homepage with clinic information
│   ├── Login.cshtml                # Authentication page
│   ├── Logout.cshtml               # Logout handler
│   ├── Patients.cshtml             # Patient list (protected)
│   ├── PatientDetail.cshtml        # Patient detail popup (protected)
│   ├── Doctors.cshtml              # Doctor directory
│   ├── DoctorDetail.cshtml         # Doctor profile popup
│   ├── Privacy.cshtml              # Privacy policy
│   └── Shared/
│       └── _Layout.cshtml          # Main layout template
├── wwwroot/
│   ├── css/                        # Stylesheets
│   ├── js/                         # JavaScript files
│   ├── lib/                        # Third-party libraries
│   └── images/
│       ├── patients/               # Patient profile photos
│       └── doctors/                # Doctor profile photos
├── Program.cs                      # Application configuration & startup
├── appsettings.json               # Configuration settings
├── healthcare.db                   # SQLite database (generated)
└── README.md                       # This file
```

## 🎨 Key Features

### 1. **Modern Homepage**
- Professional healthcare organization design
- Hero section with hospital imagery
- Service cards (Emergency Care, Diagnostics, Specialized Care, etc.)
- Medical specialties showcase
- Statistics section (50+ doctors, 15K+ patients served)
- Responsive layout

### 2. **Patient Management** (Protected)
- Comprehensive patient list with photos
- Searchable and sortable patient records
- Detailed patient profiles with medical history
- Blood type, allergies, and medication tracking
- Popup window for detailed patient information

### 3. **Doctor Directory** (Public)
- Complete doctor roster with specializations
- Professional profile photos
- Educational background and credentials
- Years of experience tracking
- Popup window for detailed doctor bios

### 4. **Secure Authentication**
- Professional login interface
- Username format validation
- Session management with automatic expiration
- Visual feedback for login errors
- User display in navigation bar when authenticated

### 5. **Responsive Design**
- Mobile-first approach
- Bootstrap 5 grid system
- Touch-friendly interface
- Optimized for all screen sizes

## 🎨 Design Highlights

### Color Scheme
- **Primary Gradient**: Purple (#667eea → #764ba2) - Used for patient-related features
- **Secondary Gradient**: Green (#11998e → #38ef7d) - Used for doctor-related features
- **Background**: Light gray (#f8f9fa) for content sections
- **Text**: Dark gray (#2c3e50) for optimal readability

### UI Components
- Gradient buttons with hover effects
- Card-based layouts with shadows
- Animated transitions and transforms
- Circular profile images with colored borders
- Badge-based status indicators

## 🔧 Configuration

### Database Connection
Located in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=healthcare.db"
  }
}
```

### Authentication Settings
Configured in `Program.cs`:
- Session Duration: 8 hours
- Sliding Expiration: Enabled
- Login Path: `/Login`
- Logout Path: `/Logout`

## 📝 Development

### Adding New Features

1. **Add a new entity model** in `Data/` folder
2. **Update ApplicationDbContext** to include the new DbSet
3. **Create Razor Pages** in `Pages/` folder
4. **Add navigation links** in `_Layout.cshtml`
5. **Apply migrations** (if needed) or rebuild database

### Running in Development Mode

```bash
dotnet run --environment Development
```

### Building for Production

```bash
dotnet publish -c Release -o ./publish
```

## 🧪 Testing the Application

1. **Homepage**: Navigate to `http://localhost:5258`
2. **View Doctors**: Click "Doctors" in navigation or "Our Doctors" button
3. **Login**: Click "Login" and use credentials from the table above
4. **View Patients**: After login, click "Patients" in navigation
5. **View Details**: Click any patient or doctor name to open detail popup
6. **Logout**: Click "Logout" in navigation when authenticated

## 📦 Dependencies

### NuGet Packages
```xml
<PackageReference Include="Azure.AI.OpenAI" Version="2.1.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0" />
```

## 🐛 Known Issues

- HTTPS port redirection warning in development (non-critical)
- Database file must be deleted manually to reset seed data
- Profile photos sourced from randomuser.me API (external dependency)

## 🔮 Future Enhancements

- [ ] Appointment scheduling system
- [ ] Patient-doctor messaging
- [ ] Medical records upload
- [ ] Prescription management
- [ ] Integration with Azure OpenAI for medical insights
- [ ] Advanced search and filtering
- [ ] Report generation
- [ ] Multi-factor authentication
- [ ] Audit logging

## 📄 License

This project is for demonstration purposes.

## 👥 Contributors

Developed with GitHub Copilot assistance.

## 📞 Support

For issues or questions, please refer to the project documentation or contact the development team.

---

**Last Updated**: January 9, 2026  
**Version**: 1.0.0  
**Framework**: ASP.NET Core 9.0
