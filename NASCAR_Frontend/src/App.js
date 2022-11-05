import React, { Component, useState, useEffect } from 'react';
import { Redirect, Route, Switch, useHistory, withRouter } from 'react-router-dom';
import { Layout } from './Components/Elements/Layout';
import  { HomePage } from './Components/Pages/HomePage';
import { Pilots } from './Components/Pages/Pilots';
import { ResultsTable } from './Components/Pages/ResultsTable';
import Tracks from './Components/Pages/Tracks';
import  AuthAdmin  from './Components/Pages/AuthAdmin';
import AddPilotsInResult from './Components/Pages/AddResult';
import AddPilot from './Components/Pages/AddPilot';
import ChangeResult from './Components/Pages/ChangeResult';

    function App(){
        const [token, setToken] = useState('');
    

        useEffect(() => {
            (
                async () => {
                    const response = await fetch('api/auth', {
                        headers: {'Content-Type': 'application/json'},
                        credentials: 'include',
                    });
    
                    const content = await response.json();
    
                    setToken(content.token);
                    console.log(token)
                }
            )();
        }, [token]);


        return(
            <Layout token={token} setToken={setToken}>
                
                    <Route exact path='/' component={HomePage} />
                    <Route path='/pilots' component={Pilots} />
                    <Route path='/table' component={ResultsTable} />
                    <Route path='/tracks' component={Tracks}/>
                    <Route path ='/auth'>
                        {token != '' ? <Redirect to="/"/> : <AuthAdmin token={token} setToken = {setToken}/>}
                    </Route>
                    <Route path='/addresult'>
                        {token != '' ? <AddPilotsInResult/> : null}
                    </Route>
                    <Route path='/addpilot'>
                        {token != '' ? <AddPilot/> : null}
                    </Route>
                    <Route path='/changeresult'>
                        {token != '' ? <ChangeResult/> : null}
                    </Route>
                
                    
            </Layout>
        )
    }

export default withRouter(App)