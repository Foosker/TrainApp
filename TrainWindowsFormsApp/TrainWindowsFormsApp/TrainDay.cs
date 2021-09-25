using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public static class TrainDay
    {
        private static int progress;
        public static List<int> indentBetweenExercises;

        private static List<ExercisesType> GetChestTrain()
        {
            indentBetweenExercises = new List<int>(){ 2, 4 };
            List<ExercisesType> train = new List<ExercisesType>();

            for (int i = 0; i < 2; i++)
            {
                train.Add(ExercisesType.ChestBase);
                train.Add(ExercisesType.DeltoidMid);
            }
            train.Add(ExercisesType.ChestIsol);
            train.Add(ExercisesType.DeltoidFront);
            return train;
        }

        private static List<ExercisesType> GetBackTrain()
        {
            indentBetweenExercises = new List<int>() { 2, 4 };
            List<ExercisesType> train = new List<ExercisesType>();

            for (int i = 0; i < 2; i++)
            {
                train.Add(ExercisesType.Latissimus);
                train.Add(ExercisesType.DeltoidRear);
            }
            train.Add(ExercisesType.Scapula);
            train.Add(ExercisesType.Biceps);
            return train;
        }

        private static List<ExercisesType> GetLegsTrain()
        {
            indentBetweenExercises = new List<int>() { 1, 3, 5 };
            var core = new List<ExercisesType>()            { ExercisesType.BackExtensor, ExercisesType.Core };
            var mainLegs = new List<ExercisesType>()        { ExercisesType.Quads, ExercisesType.Calf };
            var additionalLegs = new List<ExercisesType>()  { ExercisesType.HipBiceps, ExercisesType.Shin };
            var index = progress / 2 % 2;

            List<ExercisesType> train = new List<ExercisesType>();

            for (int i = 0; i < 2; i++)
            {
                train.Add(mainLegs[index]);
                train.Add(additionalLegs[index]);                
            }
            train.Insert(0, core[index]);
            return train;
        }

        public static List<ExercisesType> GetTrain(int _progress)
        {
            progress = _progress;

            switch (progress % 4)
            {
                case (0):
                    return GetChestTrain();
                case (2):
                    return GetBackTrain();
                default:
                    return GetLegsTrain();
            }
        }

        public static List<ExercisesType> GetWarmUpList()
        {
            var warmUp = new List<ExercisesType>()
            {
                ExercisesType.CombatArms,
                ExercisesType.CombatArms,
                ExercisesType.CombatLegs,
                ExercisesType.CombatArms,
                ExercisesType.CombatArms,
                ExercisesType.CombatLegs
            };
            return warmUp;
        }
    }
}

