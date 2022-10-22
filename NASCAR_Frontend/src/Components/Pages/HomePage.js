import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { UncontrolledCarousel, List } from 'reactstrap';
//import { Carousel } from 'react-responsive-carousel';
export class HomePage extends Component {
    static displayName = HomePage.name;

    constructor(props) {
        super(props);
        this.WelcomeCarousel = this.WelcomeCarousel.bind(this);
        this.renderHomePage = this.renderHomePage.bind(this);
        this.ListOfTopFive = this.ListOfTopFive.bind(this);
        this.state = { top5Pilots: [], loading: true };
    }

    componentDidMount() {
        this.populateHome();
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
                    <th className='text-center'>Очки</th>
                </tr>
            </thead>
            <tbody>
                {top5.map((pilot,index) => (
                    <tr key={pilot.id.toString()}>
                        <td align='center'>{index + 1}</td>
                        <td colSpan={2} align='center'>{pilot.name} {pilot.surname}</td>
                        <td align='center'>{pilot.number}</td>
                        <td align='center'>{pilot.points}</td>
                    </tr>))}
            </tbody>
            </table>
        );
    }

    WelcomeCarousel(){
        return(<div>
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
        const pilots = this.state.top5Pilots.map((pilot) => {
            <li key={pilot.id}>{pilot.name}</li>
        })

        console.log(pilots);

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

            <div className='row mt-5'>
                <div className='col-4 position-relative'>
                    <h2>Текущая турнирная таблица</h2>
                    <div className='position-absolute bottom-0 mb-4'>
                        <a className="grey-ref" href="\table">смотреть всю таблицу</a>
                    </div>
                </div>
                <div className='col'>
                    <this.ListOfTopFive/>
                </div>
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
        console.log(response);
        const data = await response.json();
        this.setState({ top5Pilots: data, loading: false });
    }
}