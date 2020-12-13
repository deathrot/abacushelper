import React, { Component } from 'react';
import AbacusSubLevels from './AbacusSubLevels';
import InputLabel from '@material-ui/core/InputLabel';

const AbacusLevels = (props) => {

    return (
      <div>
        <h1>Abacus Levels</h1>
            <AbacusSubLevels />
            <InputLabel />
      </div>
  );
};

export default AbacusLevels;