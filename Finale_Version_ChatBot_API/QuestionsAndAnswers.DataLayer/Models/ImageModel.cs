using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionsAndAnswers.DataLayer.Models
{
    public class ImageModel
    {

        public long Id { get; set; }
        public string Filename { get; set; }
        public byte[] Data { get; set; }
    }
}
