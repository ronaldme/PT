import React from 'react';
import useAxios from 'axios-hooks'
import moment from 'moment'
import WorkoutCalenderItem from './types';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';

const useStyles = makeStyles({
  table: {
    minWidth: 650,
  },
});

function WorkoutCalenderOverview() {
  const classes = useStyles();
  const [{ data, loading }] = useAxios('/WorkoutCalender');

  if (loading) return <p>Loading...</p>

  return data && (
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
            <TableCell>{moment(item.date).format("DD-MMM-YYYY")}</TableCell>
            <TableCell>{item.workout.name}</TableCell>
            <TableCell>{item.isCompleted.toString()}</TableCell>
            <TableCell></TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  </TableContainer>
  )
}

export default WorkoutCalenderOverview;
