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
            List<ExercisesType> addMuscles = new List<ExercisesType>() { ExercisesType.DeltoidMid, ExercisesType.DeltoidRear };

            for (int i = 0; i < 2; i++)
            {
                train.Add(ExercisesType.ChestIsol);
                train.Add(ExercisesType.ChestBase);
                train.Add(addMuscles[progress % addMuscles.Count]);
            }
            return train;
        }

        private static List<ExercisesType> GetCoreTrain()
        {
            indentBetweenExercises = new List<int>() { 2, };
            List<ExercisesType> train = new List<ExercisesType>();

            for (int i = 0; i < 2; i++)
            {
                train.Add(ExercisesType.Core);
                train.Add(ExercisesType.BackExtensor);
            }
            return train;
        }

        private static List<ExercisesType> GetLegsTrain()
        {
            indentBetweenExercises = new List<int>() { 2, 4 };
            List<ExercisesType> train = new List<ExercisesType>();

            for (int i = 0; i < 2; i++)
            {
                train.Add(ExercisesType.Calf);
                train.Add(ExercisesType.Quads);
            }
            train.Insert(1, ExercisesType.HipBiceps);
            train.Insert(5, ExercisesType.Shin);

            return train;
        }

        public static List<ExercisesType> GetTrain(int _progress)
        {
            progress = _progress;

            switch (progress % 4)
            {
                case (0):
                    return GetChestTrain();
                case (1):
                    return GetCoreTrain();
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

