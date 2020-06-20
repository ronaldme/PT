import React, { useState, useEffect } from 'react';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import axios from 'axios'
import DeleteIcon from '@material-ui/icons/Delete';
import { Grid, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton, Snackbar } from '@material-ui/core';

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

interface Workout {
  id: number;
  registrationDate: Date,
  weight: number,
}

export default function Overview() {
  const classes = useStyles();
  const [data, setData] = useState([]);

  function loadWorkoutList() {
    axios.get('/weightRegistration/overview', {
      params: {
        pageNumber: 1,
        pageSize: 10
      }
    }).then(function (response) {
      setData(response.data);
    })
  }

  loadWorkoutList();

  return (
    <div className={classes.root}>
      <Grid container spacing={1}>
        <Grid item xs={12}>
          <TableContainer component={Paper}>
            <Table className={classes.table}>
              <TableHead>
                <TableRow>
                  <TableCell>Weight</TableCell>
                  <TableCell>Date</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {data.map((item: Workout) => (
                  <TableRow key={item.id}>
                    <TableCell>{item.registrationDate}</TableCell>
                    <TableCell>{item.weight}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>

        </Grid>
      </Grid>
    </div>
  )
}
