import React from 'react';
import { List, ListItem, Typography, ListItemIcon, ListItemText } from '@material-ui/core';
import CheckIcon from '@material-ui/icons/Check';

export default function Home() {
  return (
    <Typography paragraph>
      Welcome to the ProgressionTracker app. Here you can track your progress for workouts. Features:

      <List dense={true}>
        <ListItem>
          <ListItemIcon>
            <CheckIcon />
          </ListItemIcon>
          <ListItemText primary="Create workouts" />
        </ListItem>

        <ListItem>
          <ListItemIcon>
            <CheckIcon />
          </ListItemIcon>
          <ListItemText primary="Workout history" />
        </ListItem>

        <ListItem>
          <ListItemIcon>
            <CheckIcon />
          </ListItemIcon>
          <ListItemText primary="Statistics" />
        </ListItem>
      </List>
    </Typography>
  );
}