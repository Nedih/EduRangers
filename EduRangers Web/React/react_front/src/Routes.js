import React from "react";
import { Route, Switch } from "react-router-dom";
import Home from "./Containers/Home";
import Login from './Containers/Login';
import Register from './Containers/Register';
import Profile from './Containers/Profile';
import Courses from './Containers/Courses';
import Сourse from './Containers/Course';
import Test from './Containers/Test';
import { Redirect } from "react-router-dom";

export default function Routes(props) {
  return (
    <Switch>
      <Route exact path="/">
        <Home />
      </Route>
      <Route exact path="/login">
        <Login />
      </Route>
      <Route exact path="/signup">
        <Register />
      </Route>
      <Route exact path="/getusers">
        <Register />
      </Route>
      <Route exact path="/deleteuser">
        <Register />
      </Route>
      <Route exact path="/courses">
        <Courses email={props.email}/>
      </Route>
      <Route exact path="/course">
        <Сourse courseid={props.courseid}/>
      </Route>
      <Route exact path="/test">
        <Test testid={props.testid}/>
      </Route>
      <Route exact path="/profile">
        <Profile email={props.email} />
      </Route>
    </Switch>
  );
}