import React, { Component } from 'react';
import { Container } from 'reactstrap';
import NavMenu from './navmenu';

const Layout = (props) => {
    return (
      <div>
        <NavMenu />
        <Container>
          {props.children}
        </Container>
      </div>
    );
}

export default Layout;