import react, {useState, useEffect} from 'react'
import {Link, withRouter, Switch, Route} from 'react-router-dom'

const Races = (props) => {
    var { path, url } = props.match;
    return(
    <div>
    <Switch>
        <Route exact path={path}>
            <RacesInfos/>
        </Route>
        <Route path={`${path}/:id`}>
            <RaceInfo />
        </Route>
    </Switch>
    </div>)
}


const RacesInfos = withRouter((props) => {
    const [stages, setStages] = useState([]);
    const [nearestStage, setNearestStage] = useState(0);

    const url = props.match.url

    useEffect(() => {
        (
            async () => {

                const response = await fetch('api/stages/all', {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                const content = await response.json();

                setStages(content.stages);
                setNearestStage(content.nearestStage);
            }
        )();
    }, []);

    const getNormalFormOfYear = (date) => {
        var year = date.substr(0,4);
        var mounth = date.substr(5,2);
        var day = date.substr(8,2);
        return (day + "." + mounth + "." + year)
    }

    return (
        <div>
            <center><h3>Расписание сезона</h3></center>
            {nearestStage != 1 ? 
                <div>
                    <h4>Прошедшие этапы:</h4>
                    <div className='row'>
                        <div className='col-5'>Этап</div>
                        <div className='col-5'>Трасса</div>
                        <div className='col-2'>Дата проведения</div>
                    </div>
                </div> 
            
            : null}

            <div className='mt-2'>
                {stages.filter(st => st.stageNumber < nearestStage).map(st => (
                    <Link to={`${url}/${st.stageNumber}`} style={{color:"black", textDecoration:"none"}}>
                    <div className=" p-2 border-bottom">
                    <div className="row">
                        <div className = "col-5" >
                            {st.name}
                        </div>
                        <div className = "col-5" >
                            {st.track.name}
                        </div>
                        <div className = "col-2" >
                            {getNormalFormOfYear(st.eventsDate)}
                        </div>
                    </div>
                    </div>
                    </Link>
                ))}
            </div>
            
            {nearestStage <= 36 ? 
                <div className='mt-5'>
                    <h4>Предстоящие гонки:</h4>
                    <div className='row'>
                        <div className='col-5'>Этап</div>
                        <div className='col-5'>Трасса</div>
                        <div className='col-2'>Дата проведения</div>
                    </div>
                </div> 
            
            : null}
            <div className='mt-2'>
                {stages.filter(st => st.stageNumber >= nearestStage).map(st => (
                    <div className="p-2 border-bottom">
                    <div className="row">
                        <div className = "col-5" >
                            {st.name}
                        </div>
                        <div className = "col-5" >
                            {st.track.name}
                        </div>
                        <div className = "col-2" >
                            {getNormalFormOfYear(st.eventsDate)}
                        </div>
                    </div>
                    </div>
                ))}
            </div>
        </div>
    )


})

const RaceInfo = withRouter((props) => {

    const [results, setResults] = useState([]);

    useEffect(() => {
        (
            async () => {
                const response = await fetch('http://localhost:3000/api/results/withNums/'+props.match.params.id, {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                const content = await response.json();

                setResults(content.results);
            }
        )();
    }, []);


    return(
    <div>
        <center><h2>Итоговый протокол {results[0]?.stage.name}</h2></center>
        <table className='table'>
            <thead>
                <tr>
                    <th className='text-center'>Место</th>
                    <th colSpan={2} className='text-center'>Пилот</th>
                    <th className='text-center'>Машина</th>
                    <th className='text-center'>Команда</th>
                    <th className='text-center'>Отставание от лидера</th>
                    <th className='text-center'>Число пит-стопов</th>
                </tr>
            </thead>
            <tbody>
                {results.map((res, index) => (
                    <tr key={index}>
                        <td align='center'>{res.place}</td>
                        <td colSpan={2} align='center'>{res.pilot.name} {res.pilot.surName}</td>
                        <td align='center'>{res.carsNumber}</td>
                        <td align='center'>{res.pilot.team.name}</td>
                        <td align='center'>{res.leaderGap}</td>
                        <td align='center'>{res.numberOfPitStops}</td>
                    </tr>))}
            </tbody>
        </table>
    </div>
    )
})

export default withRouter(Races)