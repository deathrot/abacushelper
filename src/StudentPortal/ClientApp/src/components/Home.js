import React, { Component, useContext, useEffect } from 'react';
import { AppContext, AppContextProvider } from '../context/app_context';
import Portal from './portal/portal';
import Login from './login';
import { useHistory } from "react-router-dom";
import { useLocation } from 'react-router-dom';

const Home = (props) => {
  const { state, dispatch } = useContext(AppContext);

  const location = useLocation();
  const history = useHistory();

  useEffect(() => {
    if(!state.isLoggedIn) {
      history.push({ pathname: '/Login' });
    }
  }, []);


  return (
    <div>
      <AppContextProvider>
          <Portal />
      </AppContextProvider>
    </div>
  );
}

export default Home;