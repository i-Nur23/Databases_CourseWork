import React, {useEffect, useState} from 'react'
import {useRouteMatch, withRouter, Link, Switch, Route, useParams} from 'react-router-dom'

function ChangeResult(props) {
    const { path, url } = props.match;

    return (
        <div>
            <Switch>
                <Route exact path={path}>
                    <SelectStage/>
                </Route>
                <Route path={`${path}/:Id`}>
                    <Result />
                </Route>
            </Switch>
        </div>
    )
}

const SelectStage = withRouter((props) => {
    const { path, url } = props.match;
    const [stages, setStages] = useState([]);

    useEffect(() => {
        (
            async () => {

                const response = await fetch('api/stages/getpast', {
                    headers: {'Content-Type': 'application/json'},
                });

                var content = await response.json();
                setStages(content.stages);       
            }
        )();
    }, []);

    return (
        <div>
            <center><h2>Выберете этап</h2></center>

            <ul>
            {stages.map((st)=>(
            <Link to={`${url}/${st.stageNumber}`} className='black-ref'>
                <li style={{listStyleType:'none'}}  to={`${url}/${st.stageNumber}`}>
                <div className='p-3 my-3 shadow border border-2 rounded'>
                    <h4>Этап № {st.stageNumber} : {st.name}</h4>
                    <h5>Трасса : {st.track.name}</h5>
                </div>
              </li>
            </Link>  
            ))}
        </ul>
        </div>)
})

const Result = withRouter((props) => {
    const [stageId, setStageId] = useState(0);


    useEffect(() => {
        (
            async () => {
                
                setStageId(props.match.params.Id);
                const response = await fetch('api/results/' + stageId, {
                    headers: {'Content-Type': 'application/json'},
                });

                var content = await response.json();
                setStages(content.stages);       
            }
        )();
    }, []);



  return (
    <div>
      <h3>{Id}</h3>
    </div>
  );
})

export default withRouter(ChangeResult)