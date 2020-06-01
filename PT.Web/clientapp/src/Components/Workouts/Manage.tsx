import React, { useState, useEffect } from 'react';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import axios from 'axios'
import DeleteIcon from '@material-ui/icons/Delete';
import { Grid, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton, Snackbar } from '@material-ui/core';
import AddWorkout from './AddWorkout';
import MuiAlert, { AlertProps } from '@material-ui/lab/Alert';

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
  }),
);

function Alert(props: AlertProps) {
  return <MuiAlert elevation={6} variant="filled" {...props} />;
}

interface Workout {
  id: number,
  name: string,
}

export default function Manage() {
  const classes = useStyles();
  const [data, setData] = useState([]);
  const [reload, setReload] = useState(false);
  const [openError, setOpenError] = useState(false);

  useEffect(loadWorkoutList, []);

  useEffect(() => {
    if (reload) {
      setReload(false);
      loadWorkoutList();
    }
  }, [reload]);

  if (!data) return <div>Loading...</div>;

  function loadWorkoutList() {
    axios.get('/workout/overview', {
      params: {
        pageNumber: 1,
        pageSize: 10
      }
    }).then(function (response) {
      setData(response.data);
    })
  }

  function handleDeleteClick(itemId: number) {
    axios.post('/workout/delete', { id: itemId })
      .then(loadWorkoutList)
      .catch(function () {
        setOpenError(true);
      });
  };

  const handleCloseError = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') return;
    setOpenError(false);
  };

  return (
    <div className={classes.root}>
      <Grid container spacing={1}>

        <Grid item xs={12}>
          <Paper className={classes.paper}>
            <AddWorkout setReload={setReload} />
          </Paper>
        </Grid>

        <Grid item xs={12}>
          <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell>Workout name</TableCell>
                  <TableCell>Actions</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {data.map((item: Workout) => (
                  <TableRow key={item.id}>
                    <TableCell>{item.name}</TableCell>
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

      <Snackbar open={openError} autoHideDuration={4000} onClose={handleCloseError}>
        <Alert onClose={handleCloseError} severity="error">
          Cannot delete this workout
         </Alert>
      </Snackbar>
    </div>
  )
}
