import React, { useState } from 'react';
import useAxios from 'axios-hooks'
import moment from 'moment'

interface Workout {
  name: string,
}

interface WorkoutCalenderItem {
  date: Date,
  workoutId: number,
  workout: Workout,
  isCompleted: boolean,
  remark: string,
}

function App() {
  const [{ data, loading }] = useAxios(
    '/WorkoutCalender'
  );

  if (loading) return <p>Loading...</p>
  
  if (data != null) {
    return <div>{data.map(function(item: WorkoutCalenderItem, i:number) {
      return <li key={i}>{moment(item.date).format("DD-MMM-YYYY")} - {item.workout.name}</li>
    })}</div>
  }

  return null;
}

export default App;
