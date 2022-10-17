import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './Components/Elements/Layout';
import  { HomePage } from './Components/Pages/HomePage';

        export default class App extends Component {
            static displayName = App.name;
            render(){
            return(
                <Layout>
                    <Route exact path='/'>
                        <HomePage />
                    </Route>
                    <Route exact path='/' component={HomePage} />
                </Layout>
            );
        }
    }