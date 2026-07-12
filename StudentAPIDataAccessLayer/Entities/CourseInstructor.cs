using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentAPIDataAccessLayer.Entities
{
    public class CourseInstructor
    {

        public int ID { get; set; }
        public int CourseId { get; set; }

        public int InstructorId { get; set; }

        [JsonIgnore]
        public virtual Course Course { get; set; } = null!;

        [JsonIgnore]
        public virtual Instructor Instructor { get; set; } = null!;
    }
}
