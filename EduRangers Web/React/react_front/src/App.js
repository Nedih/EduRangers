import React, {Fragment, useState } from 'react';
import './App.css';
import Routes from "./Routes";
import NavigationBar from "./Components/NavigationBar";
import { AppContext } from "./Libs/ContextLib";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { LinkContainer } from "react-router-bootstrap";
import { Redirect } from "react-router-dom";


function App(props) {
  const [isAuthenticating, setIsAuthenticating] = useState(true);
  const [isAuthenticated, userHasAuthenticated] = useState(false);
  const [userEmail, setUserEmail] = useState('');
  
  function handleLogout() {
    userHasAuthenticated(false);
    setUserEmail("");
    return (<Redirect push to="/" />)
  }
/*{userEmail, setUserEmail}*/
  return (
    <>
      <Navbar collapseOnSelect bg="dark" variant="dark" expand="md" className="mb-3">
        <LinkContainer to="/">
          <Navbar.Brand className="font-weight-bold text-muted">
            EduRangers
          </Navbar.Brand>
        </LinkContainer>
        <Navbar.Toggle />
        {isAuthenticated ? (
            <> 
          <Navbar.Collapse className="justify-content-left">
          <Nav activeKey={window.location.pathname}>
          <LinkContainer to="/courses">
            <Nav.Link>Courses</Nav.Link>
            </LinkContainer>
          </Nav>
          </Navbar.Collapse>

          <Navbar.Collapse className="justify-content-end">
          <Nav activeKey={window.location.pathname}>
            <LinkContainer to="/profile">
            <Nav.Link>Welcome, {userEmail}</Nav.Link>
            </LinkContainer>
            <Nav.Link onClick={handleLogout}>Logout</Nav.Link>
            </Nav>
          </Navbar.Collapse>
            </>
            ) : (
            <>
            <Navbar.Collapse className="justify-content-end">
          <Nav activeKey={window.location.pathname}>
                <LinkContainer to="/signup">
                <Nav.Link>Signup</Nav.Link>
                </LinkContainer>
                <LinkContainer to="/login">
                <Nav.Link>Login</Nav.Link>
                </LinkContainer>
                </Nav>
        </Navbar.Collapse>
            </>
        )}
      </Navbar>
      <AppContext.Provider value={{ isAuthenticated, userHasAuthenticated,  userEmail, setUserEmail}}> 
      <Routes email={userEmail} />
      </AppContext.Provider>
    </>
  );
}

export default App;
