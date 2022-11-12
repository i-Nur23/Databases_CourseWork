import React, { useState, useEffect } from "react";
import {withRouter, Route, Switch, Link} from "react-router-dom"

function PilotInfoChanger(props){
    var { path, url } = props.match;
    return(
    <div>
    <Switch>
        <Route exact path={path}>
            <SelectPilot/>
        </Route>
        <Route path={`${path}/:id`}>
            <ChangeInfo />
        </Route>
    </Switch>
    </div>)
}

const SelectPilot = withRouter((props) => {
    const [pilots, setPilots] = useState([]);
    const url = props.match.url;

    useEffect(() => {
        (
            async () => {
                const response = await fetch('api/pilot/all', {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                const content = await response.json();

                setPilots(content.pilots);
            }
        )();
    },[]);

    return (<div>
        <center><h2>Выберете пилота</h2></center>
        <div className="row g-3">
            {pilots.map((pilot) => (
    
                <div className=' col-4  p-2'>
                    <Link to={`${url}/${pilot.id}`} style={{color:"black", textDecoration:"none"}}>
                    <div className=" p-2 shadow border border-2 rounded">
                    <div className="d-flex justify-content-between">
                        <p className="w-75">{pilot.name} {pilot.surName}</p>
                        <p>|</p>
                        <p> {pilot.carsNumber == 0 ? null : pilot.carsNumber}</p>
                    </div>
                    <p> Команда : <strong> {pilot.team.name}</strong></p>
                    </div>
                    </Link>
                </div>
            ))}
        </div>
    </div>)

})

const ChangeInfo = withRouter((props) => {
    const [name, setName] = useState('');
    const [surname, setSurName] = useState('');
    const [date, setDate] = useState(new Date());
    const [country, setCountry] = useState('');
    const [state, setState] = useState('');
    const [city, setCity] = useState('');
    const [number, setNumber] = useState(0);
    const [status, setStatus] = useState('PT');
    const [team, setTeam] = useState(1);
    const [teamsList, setTeamsList] = useState([]);
    const [pilot, setPilot] = useState();

    const MapPilotsAttributes = async () => {
        setName(pilot.name);
        setSurName(pilot.surName);
        setDate(pilot.birthDate.substr(0,4)+'-'+pilot.birthDate.substr(5,2)+'-'+pilot.birthDate.substr(8,2));
        setCountry(pilot.birthCountry);
        setState(pilot.birthState);
        setCity(pilot.birthCity);
        setStatus(pilot.performanceStatus);
        setNumber(pilot.carsNumber);
        setTeam(pilot.teamID);        
    }

    useEffect(() => {
        (
            async () => {
                console.log(props.match.params.id);
                const path = 'https://localhost:3000/api/pilot/change/'+props.match.params.id
                console.log(path);
                const response = await fetch(path, {
                    method: 'GET',
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                },);

                const content = await response.json();
                setTeamsList(content.teams);
                var _pilot = content.pilot;
                setPilot(_pilot);
                setName(_pilot.name);
                setSurName(_pilot.surName);
                setDate(_pilot.birthDate.substr(0,4)+'-'+_pilot.birthDate.substr(5,2)+'-'+_pilot.birthDate.substr(8,2));
                setCountry(_pilot.birthCountry);
                setState(_pilot.birthState);
                setCity(_pilot.birthCity);
                setStatus(_pilot.performanceStatus);
                setNumber(_pilot.carsNumber);
                setTeam(_pilot.teamID);
            }
        )();
    },[]);

    const formChange = event => {
        var inputs = document.getElementsByClassName('required');
        Array.prototype.slice.call(inputs)
            .forEach((input) => {
                {
                    input.classList.remove("is-invalid")
                }
            })
    }

    const checkSubmitForm = async event => {
        var inputs = document.getElementsByClassName('required');
        Array.prototype.slice.call(inputs)
            .forEach((input) => {
                if (input.value === ''){
                    input.classList.add("is-invalid")
                }
            });

        await putPilot();
    }

    const putPilot = async () => {
        var pilot = {
            name : name,
            surname : surname,
            country : country,
            state : state,
            city : city,
            number : number,
            birthDate : date,
            status : status,
            team : team
        }

        var res = await fetch('https://localhost:3000/api/pilot/change/'+props.match.params.id, {
            method: 'PUT', headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify(pilot),
        });
        //props.history.push();
    }

    const resetForm = async () => {
        await MapPilotsAttributes();
    }

    const isLatinic = (key) => {
        return (/^[A-Z]+$/i.test(key) || key === '')
    }

    const isCyrillic = (key) => {
        return (/^[А-Я]+$/i.test(key) || key === '')
    }

    const is16 = (date) => {
        var now = new Date (Date.now());

        return (date.getFullYear() < now.getFullYear() || date.getMonth() < now.getMonth() || date.getDay() < now.getDay()) &&
                (now.getFullYear() - date.getFullYear() > 16)
    }

    const changeName = (e) =>{
        var key = e.target.value.slice(-1);
        if (isLatinic(key) || e.target.value.length == 1){
            setName(e.target.value);
        }
    }

    const changeSurName = (e) =>{
        var key = e.target.value.slice(-1);
        if (isLatinic(key)){
            setSurName(e.target.value);
        }
    }

    const changeDate = (e) =>{
        var birthdate = new Date(e.target.value);

        var dateInString = birthdate.getFullYear()+"-"+("0" + (birthdate.getMonth() + 1)).slice(-2)+"-"+("0" + (birthdate.getDate())).slice(-2); 
        
        if (is16(birthdate)){
            setDate(dateInString);
        }
        
    }

    const changeCountry = (e) =>{
        var key = e.target.value.slice(-1);
        if (isCyrillic(key)){
            setCountry(e.target.value);
        }
    }

    const changeState = (e) =>{
        var key = e.target.value.slice(-1);
        if (isCyrillic(key)){
            setState(e.target.value);
        }
    }

    const changeCity = (e) =>{
        var key = e.target.value.slice(-1);
        if (isCyrillic(key)){
            setCity(e.target.value);
        }
    }

    const changeNumber = (e) => {
        setNumber(e.target.value);
    }

    const changeStatus = (e) => {
        setStatus(e.target.value);
    }

    const changeTeam = (e) => {
        setTeam(e.target.value);
    }

    const SelectNum = () => {
        var nums = Array.from(Array(100).keys())
        return (
            <select value={number} class="form-select" id="numField" aria-describedby="numHelp" onChange={changeNumber}>
                {nums.map(x => 
                    <option value={x}>{x}</option>    
                )}
            </select>
        )
    }

    const SelectTeam= () => {
        return (
            <select value={team} class="form-select" id="teamField" aria-describedby="numHelp" onChange={changeTeam}>
                {teamsList.map(x => 
                    <option value={x.id}>{x.name}</option>    
                )}
            </select>
        )
    }


    return (
        <div>
            <center><h2>Данные о пилоте</h2></center>
            <div onChange={formChange}>
                <div className='row g-2'>
                    <div className='col-4 p-3'>
                        <label for="nameField" className="form-label">Имя (на латинице) </label>
                        <input type="text" value={name} class="form-control required" id="nameField" aria-describedby="nameHelp" onChange={changeName}/>
                    </div>

                    <div className='col-4 p-3'>
                        <label for="surnameField" className="form-label">Фамилия (на латинице) </label>
                        <input type="text" value={surname} class="form-control required" id="surnameField" aria-describedby="nameHelp" onChange={changeSurName}/>
                    </div>

                    <div className='col-2 p-3'>
                        <label for="dateField" className="form-label">Дата рождения </label>
                        <input type="date" value={date} class="form-control required" id="dateField" aria-describedby="nameHelp" onChange={changeDate}/>
                    </div>
                    <div className='col-2 p-3'>
                        <label for="cityField" className="form-label">Статус выступлений</label>
                        <select value={status} class="form-select" onChange={changeStatus}>
                            <option value="PT">Партаймер</option>
                            <option value="ON">Полное расписние</option>
                            <option value="OFF">Не выступает</option>
                        </select>
                    </div>
                </div>
                <div className='row g-2'>
                    <div className='col-md p-3'>
                        <label for="countryField" className="form-label">Страна</label>
                        <input type="text" value={country} class="form-control required" id="countryField" aria-describedby="nameHelp" onChange={changeCountry}/>
                    </div>

                    <div className='col-md p-3'>
                        <label for="stateField" className="form-label">Штат (если имеется)</label>
                        <input type="text" value={state} class="form-control" id="stateField" aria-describedby="nameHelp" onChange={changeState}/>
                    </div>

                    <div className='col-md p-3'>
                        <label for="cityField" className="form-label">Город </label>
                        <input type="text" value={city} class="form-control required" id="cityField" aria-describedby="nameHelp" onChange={changeCity}/>
                    </div>

                    <div className='col-sm p-3'>
                        <label for="cityField" className="form-label">Номер</label>
                        <SelectNum/>
                        <div id="numHelp" class="form-text">Оставить 0, если номер еще не определен</div>
                    </div>
                </div>
                <div className='px-2'>
                        <label for="cityField" className="form-label">Команда</label>
                        <SelectTeam/>
                    </div>
                <div className='row g-2 p-3 mt-3'>
                    <button className='btn btn-primary g-2 px-3 col-3' type="submit" onClick={checkSubmitForm}>
                        Сохранить изменения
                    </button>
                    <div className="col-6"></div>
                    <button className='btn btn-dark g-2 px-3 col-3' type="submit" onClick={resetForm}>
                        Сброс
                    </button>
                </div>
            </div>
        </div>
    )
})


export default withRouter(PilotInfoChanger)