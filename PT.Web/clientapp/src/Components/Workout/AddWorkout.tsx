import React, { useState}  from 'react';
import axios from 'axios'
import { useFormik } from 'formik';
import { Button, TextField, Grid, Snackbar } from '@material-ui/core';
import SaveIcon from '@material-ui/icons/Save';
import { makeStyles, createStyles, Theme  } from '@material-ui/core/styles';
import MuiAlert, { AlertProps } from '@material-ui/lab/Alert';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      flexGrow: 1,
    },
  }),
);

function Alert(props: AlertProps) {
  return <MuiAlert elevation={6} variant="filled" {...props} />;
}

function AddWorkout() {
  const classes = useStyles();
  const [openSuccess, setOpenSuccess] = useState(false);
  const [openError, setOpenError] = useState(false);

  const handleCloseSuccess = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') return;
    setOpenSuccess(false);
  };

  const handleCloseError = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') return;
    setOpenError(false);
  };

  const formik = useFormik({
    initialValues: {
      name: '',
    },
    onSubmit: values => {
      axios.post('/workout/add', values)
      .then(function(){
        setOpenError(false);
        setOpenSuccess(true);
        
        // TODO: Reload list
      })
      .catch(function (error) {
        setOpenSuccess(false);
        setOpenError(true);
      });
    },
  });

  return (
    <div className={classes.root}>
      <form onSubmit={formik.handleSubmit}>
        <Grid container spacing={1}>

          <Grid item xs={2}>
            <TextField
              label="Workout name"
              variant="outlined"
              size="small"
              name="name"
              onChange={formik.handleChange}
              value={formik.values.name}
            />
          </Grid>

          <Grid item xs={2}>
            <Button
              type="submit"
              variant="contained"
              color="primary"
              startIcon={<SaveIcon />}
            >
              Add
            </Button>
          </Grid>

        </Grid>
      </form>

      <Snackbar open={openSuccess} autoHideDuration={4000} onClose={handleCloseSuccess}>
        <Alert onClose={handleCloseSuccess} severity="success">
          Successfully created new workout!
        </Alert>
      </Snackbar>

      <Snackbar open={openError} autoHideDuration={4000} onClose={handleCloseError}>
        <Alert onClose={handleCloseError} severity="error">
          Cannot create duplicate workouts
        </Alert>
      </Snackbar>
    </div>
  )
}

export default AddWorkout;
