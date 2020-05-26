import React, { useState, useEffect } from 'react';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import axios from 'axios'
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';
import { Grid, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton } from '@material-ui/core';
import AddWorkout from './Workout/AddWorkout';

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
  id: number,
  name: string,
}

export default function Manage() {
  const classes = useStyles();
  const [data, setData] = useState([]);
  const [reload, setReload] = useState(false);

  useEffect(() => {
    axios.get('/workout/list',{
      params: {
        pageNumber: 1,
        pageSize: 10
      }
    }).then(function (response) {
        setData(response.data);
    })
  }, []);

  useEffect(() => {
    if (reload) {
      setReload(false);
      reloadList();
    }
  },[reload]);
  
  if (!data) return <div>Loading...</div>;

  function reloadList(){
    axios.get('/workout/list', {
      params: {
        pageNumber: 1,
        pageSize: 10
      }
    }).then(function (response) {
        setData(response.data);
    })
  }

  function click(itemId: number){
    axios.post('/workout/delete', {id: itemId}).then(reloadList);
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
                    <IconButton aria-label="edit">
                      <EditIcon />
                    </IconButton>
                    
                    <IconButton aria-label="delete"  onClick={() => click(item.id)}>
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
    </div>
  )
}
