import React, {useState, useEffect} from "react";

const ResultsTable = () => {

    const [pilots, setPilots] = useState([]);
    const [teams, setTeams] = useState([]);
    const [manufacturers, setManufacturers] = useState([]);
    const [playOffPilotsCount, setPlayOffPilotsCount] = useState();

    // yellow: rgba(201, 204, 0, 0.1);
    // red: rgba(151, 18, 18, 0.1)
    // green: rgba(4, 139, 22, 0.1)

    useEffect(() => {
        (
            async () => {
                const response = await fetch('api/results/table', {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                const content = await response.json();

                setPilots(content.pilotsRes);
                setTeams(content.teamsRes);
                setManufacturers(content.manRes);
                setPlayOffPilotsCount(content.currentRound);

            }
        )();
    }, []);

    const DrawFlag = () => {
        return(
            <img src='\Images\ChequredFlag.png' style={{height:'20px'}}/>
        )
    }

    const DrawForRegular = (props) => {
        var index = props.index;
        var pilot = props.pilot;
        if (index <= 15){
            return(<tr key={pilot.id} style={{backgroundColor:"rgba(4, 139, 22, 0.1)"}}>
                <th>{pilot.name} {pilot.surName} {pilot.hasWonInThisPlayOffRound ? <DrawFlag/> : null} </th>
                {pilot.pilotResults.map( (place,index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)
        }

        return(<tr key={pilot.id}>
            <th>{pilot.name} {pilot.surName} {pilot.hasWonInThisPlayOffRound ? <DrawFlag/> : null}</th>
            {pilot.pilotResults.map( (place,index) => (
                <th key={index} className='text-center align-middle'>
                    {place == 0 ? " - " : place}
                </th>
            ))}
            <th>{pilot.wins}</th>
            <th>{pilot.points}</th>
        </tr>)
    }

    const DrawForTop16 = (props) => {
        var index = props.index;
        var pilot = props.pilot;
        if (index <= 11){
            return(<tr key={pilot.id} style={{backgroundColor:"rgba(4, 139, 22, 0.1)"}}>
                <th>{pilot.name} {pilot.surName} {pilot.hasWonInThisPlayOffRound ? <DrawFlag/> : null}</th>
                {pilot.pilotResults.map( (place,index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)

        } else if (index <= 15){
            return(<tr key={pilot.id} style={{backgroundColor:"rgba(151, 18, 18, 0.1)"}}>
                <th>{pilot.name} {pilot.surName} {pilot.hasWonInThisPlayOffRound ? <DrawFlag/> : null}</th>
                {pilot.pilotResults.map( (place,index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)
        }

        return(<tr key={pilot.id}>
            <th>{pilot.name} {pilot.surName}</th>
            {pilot.pilotResults.map( (place,index) => (
                <th key={index} className='text-center align-middle'>
                    {place == 0 ? " - " : place}
                </th>
            ))}
            <th>{pilot.wins}</th>
            <th>{pilot.points}</th>
        </tr>)
    }

    const DrawForTop12 = (props) => {
        var index = props.index;
        var pilot = props.pilot;
        if (index <= 7){
            return(<tr key={pilot.id} style={{backgroundColor:"rgba(4, 139, 22, 0.1)"}}>
                <th>{pilot.name} {pilot.surName} {pilot.hasWonInThisPlayOffRound ? <DrawFlag/> : null}</th>
                {pilot.pilotResults.map( (place,index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)

        } else if (index <= 11){
            return(<tr key={pilot.id} style={{backgroundColor:"rgba(151, 18, 18, 0.1)"}}>
                <th>{pilot.name} {pilot.surName} {pilot.hasWonInThisPlayOffRound ? <DrawFlag/> : null}</th>
                {pilot.pilotResults.map( (place,index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)
        } else if (index <= 15) {
            return (<tr key={pilot.id} style={{backgroundColor: "rgba(201, 204, 0, 0.1)"}}>
                <th>{pilot.name} {pilot.surName}</th>
                {pilot.pilotResults.map((place, index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)
        }

        return (<tr key={pilot.id}>
            <th>{pilot.name} {pilot.surName}</th>
            {pilot.pilotResults.map((place, index) => (
                <th key={index} className='text-center align-middle'>
                    {place == 0 ? " - " : place}
                </th>
            ))}
            <th>{pilot.wins}</th>
            <th>{pilot.points}</th>
        </tr>)
    }

    const DrawForTop8 = (props) => {
        var index = props.index;
        var pilot = props.pilot;
        if (index <= 3){
            return(<tr key={pilot.id} style={{backgroundColor:"rgba(4, 139, 22, 0.1)"}}>
                <th>{pilot.name} {pilot.surName}</th>
                {pilot.pilotResults.map( (place,index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)

        } else if (index <= 7){
            return(<tr key={pilot.id} style={{backgroundColor:"rgba(151, 18, 18, 0.1)"}}>
                <th>{pilot.name} {pilot.surName}</th>
                {pilot.pilotResults.map( (place,index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)
        } else if (index <= 15) {
            return(<tr key={pilot.id} style={{backgroundColor:"rgba(201, 204, 0, 0.1)"}}>
                <th>{pilot.name} {pilot.surName}</th>
                {pilot.pilotResults.map( (place,index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)
        }

        return(<tr key={pilot.id} >
            <th>{pilot.name} {pilot.surName}</th>
            {pilot.pilotResults.map( (place,index) => (
                <th key={index} className='text-center align-middle'>
                    {place == 0 ? " - " : place}
                </th>
            ))}
            <th>{pilot.wins}</th>
            <th>{pilot.points}</th>
        </tr>)
    }

    const DrawForTop4 = (props) => {
        var index = props.index;
        var pilot = props.pilot;
        if (index <= 3) {
            return (<tr key={pilot.id} style={{backgroundColor: "rgba(149, 0, 255, 0.1)"}}>
                <th>{pilot.name} {pilot.surName}</th>
                {pilot.pilotResults.map((place, index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)
        } else if (index <= 15) {
            return (<tr key={pilot.id} style={{backgroundColor: "rgba(201, 204, 0, 0.1)"}}>
                <th>{pilot.name} {pilot.surName}</th>
                {pilot.pilotResults.map((place, index) => (
                    <th key={index} className='text-center align-middle'>
                        {place == 0 ? " - " : place}
                    </th>
                ))}
                <th>{pilot.wins}</th>
                <th>{pilot.points}</th>
            </tr>)
        }
        return (<tr key={pilot.id}>
            <th>{pilot.name} {pilot.surName}</th>
            {pilot.pilotResults.map((place, index) => (
                <th key={index} className='text-center align-middle'>
                    {place == 0 ? " - " : place}
                </th>
            ))}
            <th>{pilot.wins}</th>
            <th>{pilot.points}</th>
        </tr>)

    }

    const DrawRows = (props) => {
        switch (playOffPilotsCount){
            case 0:
                return <DrawForRegular pilot={props.pilot} index={props.index}/>
            case 16:
                return <DrawForTop16 pilot={props.pilot} index={props.index}/>
            case 12:
                return <DrawForTop12 pilot={props.pilot} index={props.index}/>
            case 8:
                return <DrawForTop8 pilot={props.pilot} index={props.index}/>
            case 4:
                return <DrawForTop4 pilot={props.pilot} index={props.index}/>
        }
    }


    const PilotsTable = () => {
        return(<div className='mt-4'>
            <ul id='table-info'>
                <li id='in-playoff-zone'><p style={{color:'black'}}> - зона попадания в следующий раунд плей-офф</p></li>
                <li id='out-playoff-zone'><p style={{color:'black'}}> - угроза вылета из плей-офф</p></li>
                <li id='elemenated-from-playoff'><p style={{color:'black'}}> - вылетевшие из плей-офф</p></li>
                <li id='four-champ'><p style={{color:'black'}}> - претенденты на титул в финальной гонке</p></li>
            </ul>
            <div className='table-responsive' style={{transform:"rotateX(180deg)"}}>
            <table className='table table-striped-columns' style={{transform:"rotateX(180deg)"}}>
            <thead>
                <tr>
                    <th>Пилот</th>
                    {new Array(36).fill(0).map((x, index) => (
                        <th>{index + 1}</th>
                    ))}
                    <th>Победы</th>
                    <th>Очки</th>
                </tr>
            </thead>
            <tbody className='table-group-divider'>
                {pilots.map((pilot ,index)=>(
                    <DrawRows pilot={pilot} index={index}/>
                ))}
            </tbody>
            </table>
            </div>
        </div>)
    }
    
    const TeamsTable = () => {
        return(<div>
            <table className='table table-striped-columns'>
                <thead>
                <tr>
                    <th>Команда</th>
                    <th>Очки</th>
                </tr>
                </thead>
                <tbody className='table-group-divider'>
                {teams.map((team ,index)=>(
                    <tr>
                        <th>{team.name}</th>
                        <th>{team.points}</th>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>)
    }

    const ManufacturersTable = () => {
        return(<div>
            <table className='table table-striped-columns'>
                <thead>
                <tr>
                    <th>Производитель</th>
                    <th>Очки</th>
                </tr>
                </thead>
                <tbody className='table-group-divider'>
                {manufacturers.map((m ,index)=>(
                    <tr>
                        <th>{m.brand}</th>
                        <th>{m.points}</th>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>)
    }


        return (
        <div>
        <ul class="nav nav-tabs" role="tablist">
            <li class="nav-item" role="presentation">
                <a class="nav-link active" data-bs-toggle="tab" href="#pilots" aria-selected="true" role="tab">Пилоты</a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" data-bs-toggle="tab" href="#teams" aria-selected="false" role="tab" tabIndex="-1">Команды</a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" data-bs-toggle="tab" href="#manufacturers" aria-selected="false" role="tab" tabindex="-1">Производители</a>
            </li>
        </ul>
    
        <div id="myTabContent" class="tab-content">
            <div class="tab-pane fade active show" id="pilots" role="tabpanel">
                <PilotsTable/>
            </div>
            <div class="tab-pane fade" id="teams" role="tabpanel">
                <TeamsTable/>
            </div>
            <div class="tab-pane fade" id="manufacturers" role="tabpanel">
                <ManufacturersTable/>
            </div>
            </div>
        </div>
    )         
}

export default ResultsTable