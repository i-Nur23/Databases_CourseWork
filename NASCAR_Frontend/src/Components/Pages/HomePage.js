import React, { Component } from 'react';
import { UncontrolledCarousel, List } from 'reactstrap';
export class HomePage extends Component {
    static displayName = HomePage.name;

    constructor(props) {
        super(props);
        this.WelcomeCarousel = this.WelcomeCarousel.bind(this);
        this.renderHomePage = this.renderHomePage.bind(this);
        this.ListOfTopFive = this.ListOfTopFive.bind(this);
        this.getFullDate = this.getFullDate.bind(this);
        this.NextRace = this.NextRace.bind(this);

        this.state = { 
            top5Pilots: [],
            nearestStage : null, 
            loading: true 
        };
    }

    componentDidMount() {
        this.populateHome();
    }

    getFullDate(){
        let date = this.state.nearestStage.eventsDate;
        let year = date.substr(0,4);
        let mounth = date.substr(5,2);
        let day = date.substr(8,2);
        switch (mounth){
            case '02':
                mounth = 'февраля';
                break;
            case '03':
                mounth = 'марта';
                break;
            case '04':
                mounth = 'апреля';
                break;
            case '05':
                mounth = 'мая';
                break;
            case '06':
                mounth = 'июня';
                break;
            case '07':
                mounth = 'июля';
                break;
            case '08':
                mounth = 'августа';
                break;
            case '09':
                mounth = 'января';
                break;
            case '10':
                mounth = 'февраля';
                break;
            case '11':
                mounth = 'марта';
                break;
        }
        return day + " " + mounth + " " + year + " г."

    }

    ListOfTopFive(){
        const top5 = this.state.top5Pilots;

        return(
            <table className='table'>
            <thead>
                <tr>
                    <th className='text-center'>Место</th>
                    <th colSpan={2} className='text-center'>Пилот</th>
                    <th className='text-center'>Машина</th>
                    <th className='text-center'>Победы</th>
                    <th className='text-center'>Очки</th>
                </tr>
            </thead>
            <tbody>
                {top5.map((pilot,index) => (
                    <tr key={pilot.id.toString()}>
                        <td align='center'>{index + 1}</td>
                        <td colSpan={2} align='center'>{pilot.name} {pilot.surname}</td>
                        <td align='center'>{pilot.number}</td>
                        <td align='center'>{pilot.wins}</td>
                        <td align='center'>{pilot.points}</td>
                    </tr>))}
            </tbody>
            </table>
        );
    }

    NextRace(){
        if (this.state.nearestStage.stageNumber !== 37){
            return (
                <div>
                <center><h3>{this.state.nearestStage.name}</h3></center>
                <br/>
                <p>Автодром: <a className='grey-ref' href="\tracks"><strong>{this.state.nearestStage.tracksName}</strong></a></p>
                <p>Дата: {this.getFullDate()}</p>
                </div>
            )
        } else {
            return (
                <div>
                    <center>Чемпионат окончен</center>
                    <h4>Чемпион: <strong>{this.state.top5Pilots[0].name}  {this.state.top5Pilots[0].surName}</strong></h4>
                </div>
            )
        }

    }

    WelcomeCarousel(){
        return(<div className="rounded">
        <UncontrolledCarousel
                items={[
                {
                    className:'welcome-carousel',
                    altText: 'Slide 1',
                    key: 1,
                    src: '\\Images\\WelcomeCarousel_1.jpg',
                },
                {
                    className:'welcome-carousel',
                    altText: 'Slide 2',
                    key: 2,
                    src: '\\Images\\WelcomeCarousel_2.jpg'
                },
                {
                    className:'welcome-carousel',
                    altText: 'Slide 3',
                    key: 3,
                    src: '\\Images\\WelcomeCarousel_3.jpg'
                },
                {
                    className:'welcome-carousel',
                    altText: 'Slide 4',
                    key: 4,
                    src: '\\Images\\WelcomeCarousel_4.jpg'
                }]} controls={false} indicators={false} autoPlay={true} interval='5000'/></div>);
    }


    renderHomePage() {

        return (
            <div className='container'>
            <div className='d-flex justify-content-center carDiv'>
                <div className='position-relative'>
                    <this.WelcomeCarousel/>
                </div>
                <div className='position-absolute'>
                    <h1 className='text-c text-dark'>Добро пожаловать в NASCAR</h1>
                </div>
            </div>

            <div className='row p-2 mt-5 shadow border border-2 rounded'>
                <div className='col-4 position-relative'>
                    <h2>Текущая турнирная таблица</h2>
                    <div className='position-absolute bottom-0 mb-4'>
                        <a className="grey-ref" href="/table">смотреть всю таблицу</a>
                    </div>
                </div>
                <div className='col'>
                    <this.ListOfTopFive/>
                </div>
            </div>

            <div className='p-2 mt-5 shadow border border-2 rounded' style={{marginLeft:"-12px",marginRight:"-12px"}}>
                <center><h2>Ближайший этап</h2></center>
                    <this.NextRace/>
            </div>
            </div>
        )            
    }

    render() {
        let contents = this.state.loading
            ? <div className='d-flex justify-content-center my-auto'>
                <p><em>Loading... please wait.</em></p>
              </div>
            : <this.renderHomePage/>;
        
        return (
            <div className='d-flex justify-content-center'>
                {contents}
            </div>
        );
    }

    async populateHome() {
        const response = await fetch('api');
        const data = await response.json();
        this.setState({ top5Pilots: data.pilots, nearestStage: data.nearestStage, loading: false });
    }
}