import React, { useState, useEffect } from 'react';
import {withRouter, Switch, Route, Link} from 'react-router-dom'
import { UncontrolledAccordion, AccordionItem ,AccordionHeader, AccordionBody } from 'reactstrap'

const Participants = withRouter((props)=>{

    const [pilots, setPilots] = useState([]);
    const [teams, setTeams] = useState([]);
    const [manufacturers, setManufs] = useState([]);

    useEffect(() => {
        (
            async () => {
                const response_pilots = await fetch('api/pilot/all', {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                var content = await response_pilots.json();

                setPilots(content.pilots);

                const response_teams = await fetch('api/team/allwithpoints', {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                content = await response_teams.json();
                setTeams (content.teams);

                const response_manufacturers = await fetch('api/manufacturer/all', {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                content = await response_manufacturers.json();
                setManufs (content.manufacturers);

            }
        )();
    },[]);

    const getNormalFormOfYear = (date) => {
        console.log(date)
        console.log(typeof(date))
        var year = date.substr(0,4);
        var mounth = date.substr(5,2);
        var day = date.substr(8,2);
        return (day + "." + mounth + "." + year)
    }

    const PilotsAccordion = () => {
        return(
            <div class="accordion accordion-flush" id="accordionExample">
                {pilots.map((pilot, index) => (
                    <div class="accordion-item" key={pilot.id}>
                    <h2 class="accordion-header" id={"heading" + index}>
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" aria-labelledby={"heading"+index} data-bs-target={`#collapse${index}`} aria-expanded="false" aria-controls={`collapse${index}`}>
                        {pilot.name} {pilot.surName}
                    </button>
                    </h2>
                    <div id={`collapse${index}`} class="accordion-collapse collapse" data-bs-parent="#accordionExample" aria-labelledby={"heading"+index}>
                        <div class="accordion-body">
                        <p><strong>Место рождения:</strong> {pilot.birthCity}, {pilot?.birthState}, {pilot.birthCountry}</p>
                        <p><strong>Дата рождения:</strong> {getNormalFormOfYear(pilot.birthDate)} ({ new Date(Date.now() - new Date(pilot.birthDate)).getUTCFullYear() - 1970 } лет)</p>
                        <p><strong>Номер машины: </strong> {pilot.carsNumber == 0 ? null : pilot.carsNumber }</p>
                        <p><strong>Команда: </strong> {pilot.team.name }</p>
                        <p><strong>Статус: </strong> {pilot.performanceStatus == 'OFF' ? "не выступает" :  pilot.performanceStatus == 'ON' ? "выступает на полном расписании" : "парт-таймер"}</p>
                        <p><strong>Плей-офф: </strong>{pilot.playOffStatus == true ? "участвует" : "не участвует"}</p>
                        <p><strong>Очки в сезоне: </strong>{pilot.points}</p>
                        <p><strong>Победы в сезоне: </strong>{pilot.wins}</p>
                        </div>
                    </div>
                    </div>
            ))}
            </div>
                
        )
    }

    const TeamsAccordion = () => {
        return(
            <div class="accordion accordion-flush" id="accordionExample">
                {teams.map((team, index) => (
                    <div class="accordion-item" key={team.id}>
                    <h2 class="accordion-header" id={"heading" + index}>
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" aria-labelledby={"heading"+index} data-bs-target={`#collapse${index}`} aria-expanded="false" aria-controls={`collapse${index}`}>
                        {team.name}
                    </button>
                    </h2>
                    <div id={`collapse${index}`} class="accordion-collapse collapse" data-bs-parent="#accordionExample" aria-labelledby={"heading"+index}>
                        <div class="accordion-body">
                        <p><strong>Год основания:</strong> {team.foundationYear}</p>
                        <p><strong>Основатель:</strong> {team.founder}</p>
                        <p><strong>Производитель:</strong> {team.manufacturer.brand}</p>
                        <p><strong>Количество очков:</strong> {team.points}</p>
                        </div>
                    </div>
                    </div>
            ))}
            </div>
                
        )
    }

    const ManufacturersAccordion = () => {
        return(
            <div class="accordion accordion-flush" id="accordionExample">
                {manufacturers.map((man, index) => (
                    <div class="accordion-item" key={man.id}>
                    <h2 class="accordion-header" id={"heading" + index}>
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" aria-labelledby={"heading"+index} data-bs-target={`#collapse${index}`} aria-expanded="false" aria-controls={`collapse${index}`}>
                        {man.brand}
                    </button>
                    </h2>
                    <div id={`collapse${index}`} class="accordion-collapse collapse" data-bs-parent="#accordionExample" aria-labelledby={"heading"+index}>
                        <div class="accordion-body">
                        <p><strong>Марка:</strong> {man.brand} {man.model}</p>
                        <p><strong>Страна:</strong> {man.brandsCountry}</p>
                        <p><strong>Количество очков:</strong> {man.points}</p>
                        </div>
                    </div>
                    </div>
            ))}
            </div>
                
        )
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
                <PilotsAccordion/>
            </div>
            <div class="tab-pane fade" id="teams" role="tabpanel">
                <TeamsAccordion/>
            </div>
            <div class="tab-pane fade" id="manufacturers" role="tabpanel">
                <ManufacturersAccordion/>
            </div>
            </div>
        </div>
    ) 
})

const ShowPilotById = withRouter((props) => {

})

export default withRouter(Participants)