import Workout from '../Workouts/types';

export default interface WorkoutCalenderItem {
  id: number,
  date: Date,
  workoutId: number,
  workout: Workout,
  isCompleted: boolean,
  distance: string,
  remark: string,
}

