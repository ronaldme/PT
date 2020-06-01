import 'date-fns';
import React, { useState, useEffect } from 'react';
import axios from 'axios'
import WorkoutCalenderItem from './types';
import DateFnsUtils from '@date-io/date-fns';
import { Formik } from 'formik';
import * as Yup from 'yup';
import {
  MuiPickersUtilsProvider,
  KeyboardDatePicker,
} from '@material-ui/pickers';
import { format } from 'date-fns'

import SaveIcon from '@material-ui/icons/Save';
import ThumbUpIcon from '@material-ui/icons/ThumbUp';
import DeleteIcon from '@material-ui/icons/Delete';
import FitnessCenterIcon from '@material-ui/icons/FitnessCenter';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import { Button, TextField, Grid, Table, TableBody, TableCell, TableContainer, Switch, TableHead, TableRow, Paper, IconButton, Snackbar } from '@material-ui/core';
import MuiAlert, { AlertProps } from '@material-ui/lab/Alert';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { green } from '@material-ui/core/colors';

interface Workout {
  id: number,
  name: string,
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      flexGrow: 1,
    },
    paper: {
      padding: theme.spacing(2),
      textAlign: 'center',
      color: theme.palette.text.secondary,
    },
    table: {
      minWidth: 650,
    },
    workoutInput: {
      marginTop: '10px'
    },
    saveButton: {
      marginTop: '20px'
    }
  }),
);

function Alert(props: AlertProps) {
  return <MuiAlert elevation={6} variant="filled" {...props} />;
}

function WorkoutCalenderOverview() {
  const classes = useStyles();
  const [data, setData] = useState<WorkoutCalenderItem[]>();
  const [workouts, setWorkouts] = useState<Workout[]>([]);
  const [reload, setReload] = useState(true);
  const [openDeleteError, setOpenDeleteError] = useState(false);
  const [selectedDate, setSelectedDate] = React.useState<Date | null>(new Date());
  const [openAddSuccess, setOpenAddSuccess] = useState(false);

  const handleCloseAddSuccess = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') return;
    setOpenAddSuccess(false);
  };

  function loadData() {
    axios.get('/workoutCalender').then((response) => setData(response.data));
    axios.get('/workout/list').then((response) => setWorkouts(response.data));
  }

  useEffect(() => {
    if (reload) {
      setReload(false);
      loadData();
    }
  }, [reload]);

  if (!data) return <div>Loading...</div>;

  function handleChange(e: React.ChangeEvent<HTMLInputElement>, item: WorkoutCalenderItem) {
    if (!data) return;

    const index = data.findIndex(element => element.id == item.id);
    let newArray = [...data];

    const newIsCompleted = !item.isCompleted;
    newArray[index] = { ...newArray[index], isCompleted: newIsCompleted };
    setData(newArray);

    axios.post('/workoutCalender/toggleIsCompleted', {
      workoutCalenderItemId: item.id,
      isCompleted: newIsCompleted,
    });
  }

  function handleDeleteClick(itemId: number) {
    axios.post('/workoutCalender/delete', { id: itemId })
      .then(() => setReload(true))
      .catch(() => setOpenDeleteError(true));
  };

  const handleCloseError = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') return;
    setOpenDeleteError(false);
  };

  return (
    <div className={classes.root}>
      <Grid container spacing={1}>

        <Grid item xs={12}>
          <Paper className={classes.paper}>
            <Grid container spacing={1}>
              <Formik
                initialValues={{ workoutId: '', date: new Date(), }}
                validationSchema={Yup.object({
                  workoutId: Yup.number()
                    .min(1, 'Please select a workout')
                    .required('Required'),
                  date: Yup.date().required('Required')
                })}
                onSubmit={(values, { setSubmitting }) => {
                  setSubmitting(false);
                  axios.post('/workoutCalender/add', values).then(() => setReload(true))
                }}
              >
                {formik => (
                  <div className={classes.root}>
                    <form onSubmit={formik.handleSubmit}>
                      <Grid container spacing={1}>

                        <Grid item xs={3}>
                          <MuiPickersUtilsProvider utils={DateFnsUtils}>
                            <KeyboardDatePicker
                              disableToolbar
                              variant="inline"
                              format="MM/dd/yyyy"
                              margin="normal"
                              id="date-picker-inline"
                              name="date"
                              label="Workout date"
                              value={selectedDate}
                              onChange={function (e, item) {
                                if (!item) return;
                                var date = new Date(item);
                                setSelectedDate(date);
                                formik.setFieldValue('date', date);
                              }}
                              KeyboardButtonProps={{
                                'aria-label': 'change date',
                              }}
                            />
                          </MuiPickersUtilsProvider>
                        </Grid>

                        <Grid item xs={3}>
                          <Autocomplete
                            id="workoutId"
                            options={workouts}
                            className={classes.workoutInput}
                            getOptionLabel={(option) => option.name}
                            style={{ width: 300 }}
                            onChange={function (e, item) {
                              if (!item) return;
                              formik.setFieldValue('workoutId', item.id);
                            }}
                            renderInput={(params) =>
                              <TextField
                                {...params}
                                label="Workout"
                                variant="outlined"
                              />
                            }
                          />
                        </Grid>

                        <Grid item xs={2}>
                          <Button
                            className={classes.saveButton}
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
                  </div>
                )}
              </Formik>
            </Grid>
          </Paper>
        </Grid>

        <Grid item xs={12}>
          <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell>Date</TableCell>
                  <TableCell>Workout</TableCell>
                  <TableCell>Is completed</TableCell>
                  <TableCell>Action</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {data.map((item: WorkoutCalenderItem) => (
                  <TableRow key={item.id}>
                    <TableCell>{format(new Date(item.date), "dd-MMM-yyyy")}</TableCell>
                    <TableCell>{item.workout.name}</TableCell>
                    <TableCell>{item.isCompleted ? <ThumbUpIcon style={{ color: green[500] }} /> : <FitnessCenterIcon />}

                      <Switch
                        checked={item.isCompleted}
                        onChange={(e) => handleChange(e, item)}
                        name="checkedA"
                        inputProps={{ 'aria-label': 'secondary checkbox' }}
                      />

                    </TableCell>
                    <TableCell>
                      <IconButton aria-label="delete" onClick={() => handleDeleteClick(item.id)}>
                        <DeleteIcon />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>

        </Grid>
      </Grid>

      <Snackbar open={openAddSuccess} autoHideDuration={4000} onClose={handleCloseAddSuccess}>
        <Alert onClose={handleCloseAddSuccess} severity="success">
          Successfully created new workout calender item!
         </Alert>
      </Snackbar>

      <Snackbar open={openDeleteError} autoHideDuration={4000} onClose={handleCloseError}>
        <Alert onClose={handleCloseError} severity="error">
          Cannot delete completed workouts
         </Alert>
      </Snackbar>
    </div>
  )
};

export default WorkoutCalenderOverview;
