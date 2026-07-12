using Microsoft.EntityFrameworkCore;
using StudentAPIDataAccessLayer.Entities;

namespace Students_Management_System.Data_Access_Layer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Register entites inside DbContext
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        public DbSet<CourseDepartment> CourseDepartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================
            // Student Configuration
            // ==========================
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(s => s.IsActive)
                .IsRequired();

                entity.Property(s => s.Age)
                .IsRequired();

                entity.Property(s => s.Email)
                      .HasMaxLength(100);

                entity.HasIndex(e => e.Email, "UQ__Students")
                .IsUnique();

                entity.Property(e => e.EnrollmentDate)
                     .HasDefaultValueSql("(GETDATE())");


            });

            // ==========================
            // Course Configuration
            // ==========================
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasIndex(c => c.Code, "UQ__Courses")
                    .IsUnique();


                entity.Property(c => c.Code)
                    .IsRequired()
                    .HasMaxLength(20);


                entity.Property(c => c.Credits)
                .IsRequired();

                entity.Property(c => c.Title)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Description)
                      .HasMaxLength(500);


                entity.ToTable(t =>
                {
                    t.HasCheckConstraint(
                        "CHK_Credit",
                        "[Credits] IN (1,3,6)");
                });
            });

            // ==========================
            // Enrollment Configuration
            // ==========================
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Grade).IsRequired();

                entity.Property(e => e.AttendanceRate)
                      .HasPrecision(5, 2).IsRequired();

                entity.Property(e => e.Semester)
                      .HasMaxLength(20).IsRequired();

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint(
                        "CK__Enrollment__Grade",
                        "[Grade] >= 0 AND [Grade] <= 100");

                    t.HasCheckConstraint(
                        "CK__Enrollment__Attendance",
                        "[AttendanceRate] >= 0 AND [AttendanceRate] <= 100");
                });

                // Enrollment -> Student
                entity.HasOne(e => e.Student)
                      .WithMany(s => s.Enrollments)
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Cascade);

                // Enrollment -> Course
                entity.HasOne(e => e.Course)
                      .WithMany(c => c.Enrollments)
                      .HasForeignKey(e => e.CourseID)
                      .OnDelete(DeleteBehavior.Cascade);



                // ==========================
                // Department Configuration
                // ==========================

                modelBuilder.Entity<Department>(entity =>
                {
                    entity.HasKey(d => d.Id);

                    entity.Property(d => d.Name)
                          .IsRequired()
                          .HasMaxLength(100);

                    entity.Property(d => d.Description)
                          .HasMaxLength(300);

                    entity.HasIndex(d => d.Name)
                          .IsUnique();
                });


                // ==========================
                // Instructor Configuration
                // ==========================


                modelBuilder.Entity<Instructor>(entity =>
                {
                    entity.HasKey(i => i.Id);

                    entity.Property(i => i.FirstName)
                          .IsRequired()
                          .HasMaxLength(50);

                    entity.Property(i => i.LastName)
                          .IsRequired()
                          .HasMaxLength(50);

                    entity.Property(i => i.Email)
                          .IsRequired()
                          .HasMaxLength(255);

                    entity.HasIndex(i => i.Email)
                          .IsUnique();

                    entity.Property(i => i.Phone)
                          .HasMaxLength(20);

                    entity.Property(i => i.Office)
                          .HasMaxLength(50);

                    entity.Property(i => i.Salary)
                          .HasColumnType("decimal(10,2)");

                    entity.Property(i => i.IsActive)
                          .HasDefaultValue(true);

                    entity.HasOne(i => i.Department)
                          .WithMany(d => d.Instructors)
                          .HasForeignKey(i => i.DepartmentId)
                          .OnDelete(DeleteBehavior.Restrict);


                    // ==========================
                    // CourseInstructor Configuration
                    // ==========================



                    modelBuilder.Entity<CourseInstructor>(entity =>
                    {
                        entity.HasKey(ci => new
                        {
                            ci.CourseId,
                            ci.InstructorId
                        });

                        entity.HasOne(ci => ci.Course)
                              .WithMany(c => c.CourseInstructors)
                              .HasForeignKey(ci => ci.CourseId);

                        entity.HasOne(ci => ci.Instructor)
                              .WithMany(i => i.CourseInstructors)
                              .HasForeignKey(ci => ci.InstructorId);
                    });
                });




                // 1. تحديد الـ Id كمفتاح أساسي وحيد
                modelBuilder.Entity<CourseDepartment>()
                    .HasKey(cd => cd.Id);
                // 2. إضافة قيد الفرادة (Unique Index) لمنع تكرار نفس المادة لنفس القسم
                modelBuilder.Entity<CourseDepartment>()
                    .HasIndex(cd => new { cd.CourseId, cd.DepartmentId })
                    .IsUnique();
                // 3. تحديد العلاقات والمفاتيح الخارجية (Foreign Keys)
                modelBuilder.Entity<CourseDepartment>()
                    .HasOne(cd => cd.Course)
                    .WithMany(c => c.CourseDepartments)
                    .HasForeignKey(cd => cd.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.Entity<CourseDepartment>()
                    .HasOne(cd => cd.Department)
                    .WithMany(d => d.CourseDepartments)
                    .HasForeignKey(cd => cd.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}