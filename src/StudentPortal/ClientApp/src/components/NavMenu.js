import React, { Component, useContext } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { AppContext } from '../context/app_context';

const NavMenu = (props) => {

  const state = useContext(AppContext);

    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">StudentPortal</NavbarBrand>
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                {!state.isLoggedIn &&
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/Login">Login</NavLink>
                </NavItem>
                }
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
}

export default NavMenu;