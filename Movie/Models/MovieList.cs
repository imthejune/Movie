using System.ComponentModel.DataAnnotations;

namespace Movie.Models
{
    public class MovieList
    {
        [Key]
        public int MovieID { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อภาพยนต์")]
        public string MovieTitle { get; set; }


        public string MovieImg { get; set; }

        [Required(ErrorMessage = "กรุณากรอกวันเข้าฉาย")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกหมวดหมู่")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "กรุณากรอกความยาว (นาที)")]
        public int Duration { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}
