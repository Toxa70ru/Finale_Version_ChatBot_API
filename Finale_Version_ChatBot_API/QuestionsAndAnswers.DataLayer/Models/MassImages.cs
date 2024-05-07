using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionsAndAnswers.DataLayer.Models
{
    public class MassImages
    {
        public long Id { get; set; }
        public int? FirstPictureId { get; set; }
        public int? SecondPictureId { get; set; }
        public int? ThirdPictureId { get; set; }

    }
}
