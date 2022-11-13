import react, {useState, useEffect} from 'react'
import {Link, withRouter} from 'react-router-dom'

const Races = () => {
    const [stages, setStages] = useState([]);
    const [nearestStage, setNearestStage] = useState(0);

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


}

export default withRouter(Races)