import React from 'react';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";
import WorkoutCalenderOverview from './WorkoutCalenderItem/WorkoutCalenderOverview';

function App() {
  return (
    <Router>
      <div>
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/workoutCalenderOverview">Workout calender</Link>
            </li>
          </ul>
        </nav>

        <Switch>
          <Route path="/workoutCalenderOverview">
            <WorkoutCalenderOverview />
          </Route>
          <Route path="/">
            <Home />
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

function Home() {
  return <h2>Welcome to the PT app</h2>;
}

export default App;
