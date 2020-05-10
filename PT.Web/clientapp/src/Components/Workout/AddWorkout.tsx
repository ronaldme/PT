import React from 'react';
import axios from 'axios'
import { useFormik } from 'formik';

function AddWorkout() {
  const formik = useFormik({
    initialValues: {
      name: '',
    },
    onSubmit: values => {
      axios.post('/workout/add', values);
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

export default AddWorkout;
