import React from 'react';
import axios from 'axios'
import { useFormik } from 'formik';

function EditWorkout({id} : { id: number}) {
  const formik = useFormik({
    initialValues: {
      id: id,
      name: '',
    },
    onSubmit: values => {
      axios.post('/workout/edit', values);
    },
  });

  return (
    <form onSubmit={formik.handleSubmit}>
      <label htmlFor="name">Workout name</label>
      <input
        id="name"
        name="name"
        onChange={formik.handleChange}
        value={formik.values.name}
      />
      <button type="submit">Save workout</button>
    </form>
  );}

export default EditWorkout;
