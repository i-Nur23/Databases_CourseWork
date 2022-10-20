import React, { Component } from 'react';

export class HomePage extends Component {
    static displayName = HomePage.name;

    constructor(props) {
        super(props);
        this.state = { top5Pilots: [], loading: true };
    }

    componentDidMount() {
        this.populateHome();
    }

    static renderHomePage(top5Pilots) {
        return (
            <div><h1>Welcome</h1></div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <div className='d-flex justify-content-center my-auto'>
                <p><em>Loading... please wait.</em></p>
              </div>
            : HomePage.renderHomePage(this.state.top5Pilots);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateHome() {
        const response = await fetch('api');
        console.log(response);
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}