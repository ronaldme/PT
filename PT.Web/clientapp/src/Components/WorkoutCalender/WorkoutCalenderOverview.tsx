import 'date-fns';
import React, { useState, useEffect } from 'react';
import axios from 'axios'
import WorkoutCalenderItem from './types';
import DateFnsUtils from '@date-io/date-fns';
import { Formik, Field } from 'formik';
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
import { makeStyles, createStyles, Theme, withStyles, WithStyles } from '@material-ui/core/styles';
import { Button, Grid, Table, TableBody, TableCell, TableContainer, Switch, TableHead, TableRow, Paper, IconButton, Snackbar, TextField as MuiTextField } from '@material-ui/core';
import MuiAlert, { AlertProps } from '@material-ui/lab/Alert';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { green } from '@material-ui/core/colors';
import { TextField } from 'formik-material-ui';
import ChatBubbleIcon from '@material-ui/icons/ChatBubble';
import Dialog from '@material-ui/core/Dialog';
import MuiDialogTitle from '@material-ui/core/DialogTitle';
import MuiDialogContent from '@material-ui/core/DialogContent';
import MuiDialogActions from '@material-ui/core/DialogActions';
import CloseIcon from '@material-ui/icons/Close';
import Typography from '@material-ui/core/Typography';

const styles = (theme: Theme) =>
  createStyles({
    root: {
      margin: 0,
      padding: theme.spacing(2),
    },
    closeButton: {
      position: 'absolute',
      right: theme.spacing(1),
      top: theme.spacing(1),
      color: theme.palette.grey[500],
    },
  });

interface DialogTitleProps extends WithStyles<typeof styles> {
  id: string;
  children: React.ReactNode;
  onClose: () => void;
}

const DialogTitle = withStyles(styles)((props: DialogTitleProps) => {
  const { children, classes, onClose, ...other } = props;
  return (
    <MuiDialogTitle disableTypography className={classes.root} {...other}>
      <Typography variant="h6">{children}</Typography>
      {onClose ? (
        <IconButton aria-label="close" className={classes.closeButton} onClick={onClose}>
          <CloseIcon />
        </IconButton>
      ) : null}
    </MuiDialogTitle>
  );
});

const DialogContent = withStyles((theme: Theme) => ({
  root: {
    padding: theme.spacing(2),
  },
}))(MuiDialogContent);

const DialogActions = withStyles((theme: Theme) => ({
  root: {
    margin: 0,
    padding: theme.spacing(1),
  },
}))(MuiDialogActions);


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
    inputs: {
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

  const [open, setOpen] = React.useState(false);
  const [selectedId, setSelectedId] = useState<number>();
  const [selectedRemark, setSelectedRemark] = useState<string>();

  const handleClickOpen = (id: number, remark: string) => {
    setSelectedId(id); // TODO: To objects
    setSelectedRemark(remark);
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
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
                initialValues={{ workoutId: '', date: new Date(), distance: null }}
                validationSchema={Yup.object({
                  workoutId: Yup.number()
                    .min(1, 'Please select a workout')
                    .required('Required'),
                  date: Yup.date().required('Required'),
                  distance: Yup.number().nullable()
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
                            className={classes.inputs}
                            getOptionLabel={(option) => option.name}
                            style={{ width: 300 }}
                            onChange={function (e, item) {
                              if (!item) return;
                              formik.setFieldValue('workoutId', item.id);
                            }}
                            renderInput={(params) =>
                              <MuiTextField
                                {...params}
                                label="Workout"
                                variant="outlined"
                              />
                            }
                          />
                        </Grid>

                        <Grid item xs={3}>
                          <Field
                            component={TextField}
                            className={classes.inputs}
                            type="number"
                            label="Distance (km)"
                            name="distance"
                            variant="outlined"
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
                  <TableCell>Distance</TableCell>
                  <TableCell>Remark</TableCell>
                  <TableCell>Actions</TableCell>
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
                    <TableCell>{item.distance} {item.distance ? ' km' : '-'}</TableCell>
                    <TableCell>{item.remark}</TableCell>
                    <TableCell>
                      <IconButton aria-label="addRemark" onClick={() => handleClickOpen(item.id, item.remark)}>
                        <ChatBubbleIcon />
                      </IconButton>
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

      <Dialog onClose={handleClose} aria-labelledby="customized-dialog-title" open={open} fullWidth={true} maxWidth={'sm'}>
        <DialogTitle id="customized-dialog-title" onClose={handleClose}>
          Add remark to the workout
        </DialogTitle>

        <Formik
          initialValues={{ id: selectedId, remark: selectedRemark }}
          validationSchema={Yup.object({
            id: Yup.number().required('Required'),
            remark: Yup.string().required('Required'),
          })}
          onSubmit={(values, { setSubmitting }) => {
            setSubmitting(false);
            axios.post('/workoutCalender/addRemark', values).then(() => setReload(true))
            handleClose();
          }}
        >
          {formik => (
            <div className={classes.root}>
              <form onSubmit={formik.handleSubmit}>
                <DialogContent dividers>
                  <Field
                    component={TextField}
                    name="remark"
                    id="outlined-multiline-static"
                    label="Remark"
                    multiline
                    rows={4}
                    fullWidth
                    variant="outlined"
                  />
                </DialogContent>

                <DialogActions>
                  <Button autoFocus color="primary" type="submit">
                    Save
                  </Button>
                </DialogActions>
              </form>
            </div>
          )}
        </Formik>

      </Dialog>
    </div>
  )
};

export default WorkoutCalenderOverview;
