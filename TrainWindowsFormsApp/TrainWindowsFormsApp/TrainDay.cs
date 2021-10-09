using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrainWindowsFormsApp
{
    public static class TrainDay
    {
        private static int progress;
        public static List<int> indentBetweenExercises = new List<int>();
        private static List<ExercisesType> train = new List<ExercisesType>();
        //
        // Для тренировок груди
        //
        private static List<ExercisesType> forChest = new List<ExercisesType> { ExercisesType.ChestBase, ExercisesType.ChestIsol, 
                                                                                ExercisesType.DeltoidMid, ExercisesType.DeltoidFront};        
        private static List<ExercisesType> GetChestTrain()
        {
            for (int i = 0; i < 2; i++)
            {
                train.Add(forChest[0]);
                train.Add(forChest[2]);
            }

            if (TrainCommon.option == "strength")
            {
                indentBetweenExercises.AddRange(new int[] { 1, 2, 3 });
            }
            else if (TrainCommon.option == "stamina")
            {
                indentBetweenExercises.AddRange(new int[] { 2, 4 });
                train.Add(forChest[1]);
                train.Add(forChest[3]);
            }
            else
            {
                indentBetweenExercises.AddRange(new int[] { 1, 2, 3, 4 });
                train.Add(forChest[1]);
            }

            return train;
        }
        //
        // Для тренировок спины
        //
        private static List<ExercisesType> forBack = new List<ExercisesType> { ExercisesType.Latissimus, ExercisesType.Scapula, 
                                                                               ExercisesType.DeltoidRear, ExercisesType.Biceps};
        private static List<ExercisesType> GetBackTrain()
        {
            for (int i = 0; i < 2; i++)
            {
                train.Add(forBack[0]);
            }

            if (TrainCommon.option == "strength")
            {
                indentBetweenExercises.AddRange(new int[] { 1, 2, 3 });
                train.Add(forBack[2]);
                train.Add(forBack[3]);
            }
            else
            {
                indentBetweenExercises.AddRange(new int[] { 2, 4 });
                train.Insert(1, forBack[2]);
                train.Add(forBack[2]);
                train.Add(forBack[1]);
                train.Add(forBack[3]);
            }
            if (TrainCommon.option == "tabata")
            {
                indentBetweenExercises.AddRange(new int[] { 1, 3 });
                indentBetweenExercises.Sort();
                train.RemoveAt(4);
            }
            return train;
        }
        //
        // Для тренировок ног и кора
        //
        private static List<ExercisesType> forLegs = new List<ExercisesType> { ExercisesType.Quads, ExercisesType.Calf,
                                                                               ExercisesType.HipBiceps, ExercisesType.Shin };
        private static List<ExercisesType> forCore = new List<ExercisesType> { ExercisesType.BackExtensor, ExercisesType.Core };

        private static List<ExercisesType> GetLegsTrain()
        {
            var index = progress / 2 % 2;
            train.Add(forLegs[index]);
            train.Add(forLegs[index + 2]);
            train.Add(forCore[index]);

            train.Add(forLegs[index]);
            train.Add(forCore[index]);
            if (TrainCommon.option == "stamina")
            {
                indentBetweenExercises.AddRange(new int[] { 3 });
            }
            else
            {
                indentBetweenExercises.AddRange(new int[] { 1, 2, 3, 4 });
            }
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

