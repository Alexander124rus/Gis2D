mapboxgl.accessToken = 'pk.eyJ1IjoibWloYXlsZW5rbzAwMSIsImEiOiJja3Y3czd1cjM1ZWlhMnVzN2g1NTMzbGF4In0.hb6-e4kqrfQ7ZrkHfidJdA';
const filterGroup = document.getElementById('filter-group');

const map = new mapboxgl.Map({
    container: 'map',
    //style: 'mapbox://styles/mapbox/light-v10',
    style: 'mapbox://styles/mihaylenko001/ckv972u076uwv14nx9amcctyn',
    center: [92.87, 56.013],
    zoom: 2.15
});

var places;

const getData = async url => {
    const response = await fetch(url)
    const data = await response.json()
    return data
}

getData('./js/geomarkers.json')
    .then(data => {
        // передаем данные функции создания теста
        places = data;
    });

map.on('load', () => {
    // Добавьте источник GeoJSON, содержащий координаты места и информацию.
    map.addSource('places', {
        'type': 'geojson',
        'data': './js/geomarkers.json'
    });

    for (const feature of places.features) {
        const symbol = feature.properties.layers;
        const layerID = `poi-${symbol}`;
        const iconImage = feature.properties.image;

        // Добавьте слой для этого типа символов, если он еще не был добавлен.
        if (!map.getLayer(layerID)) {


            map.addLayer({
                'id': layerID,
                'type': 'symbol',
                'source': 'places',
                'layout': {
                    //Эти значки являются частью светового стиля Mapbox.
                    // Чтобы просмотреть все изображения, доступные в стиле картографического поля, откройте
                    // стиль в Mapbox Studio и перейдите на вкладку "Изображения".
                    //Чтобы добавить новое изображение в стиль во время выполнения, см.
                    // https://docs.mapbox.com/mapbox-gl-js/example/add-image/
                    'icon-image': iconImage,
                    'icon-allow-overlap': true
                },
                'filter': ['==', 'layers', symbol]
            });

            //Когда событие щелчка происходит на объекте в слое "Места", откройте всплывающее окно в
            //местоположении объекта с описанием HTML из его свойств.
            map.on('click', layerID, (e) => {
                // Copy coordinates array.
                const coordinates = e.features[0].geometry.coordinates.slice();
                const description = e.features[0].properties.description;

                // Убедитесь, что если карта уменьшена таким образом, чтобы несколько
                //копии функции видны, появляется всплывающее окно
                //над копией, на которую указывают.
                while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                    coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
                }

                new mapboxgl.Popup()
                    .setLngLat(coordinates)
                    .setHTML(description)
                    .setMaxWidth("1200px")
                    .addTo(map);
            });

            // Измените курсор на указатель, когда мышь находится над слоем "Места".
            map.on('mouseenter', layerID, () => {
                map.getCanvas().style.cursor = 'pointer';
            });

            // Измените его обратно на указатель, когда он уйдет.
            map.on('mouseleave', layerID, () => {
                map.getCanvas().style.cursor = '';
            });

            // Добавьте элементы флажка и метки для слоя.
            const input = document.createElement('input');
            input.type = 'checkbox';
            input.id = layerID;
            input.checked = true;
            filterGroup.appendChild(input);

            const label = document.createElement('label');
            label.setAttribute('for', layerID);
            label.textContent = feature.properties.name_ru;
            filterGroup.appendChild(label);

            // Когда флажок изменится, обновите видимость слоя.
            input.addEventListener('change', (e) => {
                map.setLayoutProperty(
                    layerID,
                    'visibility',
                    e.target.checked ? 'visible' : 'none'
                );
            });
        }
    }
});


    

    