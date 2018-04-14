using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class ExerciseListShowViewModel
    {


        public IEnumerable<Exercise> Exercises { get; set; }

        public ExerciseListViewModel ExerciseViewModel { get; set; }
    }
}
