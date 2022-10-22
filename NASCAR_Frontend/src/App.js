import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './Components/Elements/Layout';
import  { HomePage } from './Components/Pages/HomePage';
import { Pilots } from './Components/Pages/Pilots';
import { ResultsTable } from './Components/Pages/ResultsTable';

        export default class App extends Component {
            static displayName = App.name;
            render(){
            return(
                <Layout>
                    <Route exact path='/' component={HomePage} />
                    <Route path='/pilots' component={Pilots} />
                    <Route path='/table' component={ResultsTable} />
                </Layout>
            );
        }
    }