import React, { Component, useState, useEffect } from 'react';
import { Redirect, Route, Switch, useHistory, withRouter } from 'react-router-dom';
import { Layout } from './Components/Elements/Layout';
import { HomePage } from './Components/Pages/HomePage';
import Participants  from './Components/Pages/Pilots';
import ResultsTable from './Components/Pages/ResultsTable';
import Tracks from './Components/Pages/Tracks';
import AuthAdmin  from './Components/Pages/AuthAdmin';
import AddPilotsInResult from './Components/Pages/AddResult';
import AddPilot from './Components/Pages/AddPilot';
import PilotInfoChanger from './Components/Pages/PilotInfoChanger';
import ChangeNumber from './Components/Pages/ChangeNumber';
import Races from './Components/Pages/Races';
import RaceInfo from './Components/Pages/RaceInfo';

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
                }
            )();
        }, [token]);


        return(
            <Layout token={token} setToken={setToken}>
                
                    <Route exact path='/' component={HomePage} />
                    <Route path='/participants'>
                        <Participants/>
                    </Route>
                    <Switch>
                    <Route path='/races'>
                        <Races/>
                    </Route>
                    <Route path='/races/:id'>
                        <RaceInfo/>
                    </Route>
                    </Switch>
                    <Route path='/table'>
                        <ResultsTable/>
                    </Route>
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
                    <Route path='/changepilotinfo'>
                        {token != '' ? <PilotInfoChanger/> : null}
                    </Route>
                    <Route path='/changenum'>
                        {token != '' ? <ChangeNumber/> : null}
                    </Route>
            </Layout>
        )
    }

export default withRouter(App)