import React, { Component, useContext } from 'react';
import { AppContext } from '../context/app_context';
import Portal from './portal/portal';
import Welcome from './welcome';

const Home = (props) => {
    const {state, dispatch} = useContext(AppContext);

    return (
      <div>
        <Portal />
      </div>
    );
}

export default Home;