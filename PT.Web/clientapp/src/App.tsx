import React from 'react';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";

import { AppBar, CssBaseline, Drawer, List, ListItem, Divider, Toolbar, Typography, ListItemIcon, ListItemText } from '@material-ui/core';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import AssessmentIcon from '@material-ui/icons/Assessment';
import BuildIcon from '@material-ui/icons/Build';
import ScheduleIcon from '@material-ui/icons/Schedule';
import HomeIcon from '@material-ui/icons/Home';

import Home from './Home';
import WorkoutCalenderOverview from './Components/WorkoutCalender/WorkoutCalenderOverview';
import Manage from './Components/Workouts/Manage';
import Statistics from './Components/Statistics/Statistics';
import Overview from './Components/WeightRegistration/Overview';

const drawerWidth = 240;
const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
    },
    appBar: {
      width: `calc(100% - ${drawerWidth}px)`,
      marginLeft: drawerWidth,
    },
    drawer: {
      width: drawerWidth,
      flexShrink: 0,
    },
    drawerPaper: {
      width: drawerWidth,
    },
    toolbar: theme.mixins.toolbar,
    content: {
      flexGrow: 1,
      backgroundColor: theme.palette.background.default,
      padding: theme.spacing(3),
    },
  })
);

function App() {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <Router>
        <CssBaseline />
        <AppBar position="fixed" className={classes.appBar}>
          <Toolbar>
            <Typography variant="h6" noWrap>
              Progression tracker
            </Typography>
          </Toolbar>
        </AppBar>
        <Drawer
          className={classes.drawer}
          variant="permanent"
          classes={{
            paper: classes.drawerPaper,
          }}
          anchor="left"
        >
          <div className={classes.toolbar} />
          <Divider />
          <List>
            <ListItem component={props => <Link {...props} to="/" />}>
              <ListItemIcon><HomeIcon /></ListItemIcon>
              <ListItemText primary="Home" />
            </ListItem>

            <ListItem component={props => <Link {...props} to="/calender" />}>
              <ListItemIcon><ScheduleIcon /></ListItemIcon>
              <ListItemText primary="Workout calender" />
            </ListItem>
            
            <ListItem component={props => <Link {...props} to="/manage" />}>
              <ListItemIcon><BuildIcon /></ListItemIcon>
              <ListItemText primary="Manage workouts" />
            </ListItem>

            <ListItem component={props => <Link {...props} to="/weightRegistration" />}>
              <ListItemIcon><BuildIcon /></ListItemIcon>
              <ListItemText primary="Weight registration" />
            </ListItem>

            <ListItem component={props => <Link {...props} to="/statistics" />}>
              <ListItemIcon><AssessmentIcon /></ListItemIcon>
              <ListItemText primary="Statistics" />
            </ListItem>
          </List>
        </Drawer>
        <main className={classes.content}>
          <div className={classes.toolbar} />
          <Switch>
            <Route path="/statistics">
              <Statistics />
            </Route>
            <Route path="/manage">
              <Manage />
            </Route>
            <Route path="/weightRegistration">
              <Overview />
            </Route>
            <Route path="/calender">
              <WorkoutCalenderOverview />
            </Route>
            <Route path="/">
              <Home />
            </Route>
          </Switch>
        </main>
      </Router>
    </div>
  );
}

export default App;
