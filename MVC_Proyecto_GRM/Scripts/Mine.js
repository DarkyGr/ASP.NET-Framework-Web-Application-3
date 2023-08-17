let app = document.getElementById('typewriter');

let typewriter = new Typewriter(app, {
    loop: true,
    delay: 100,
});

typewriter
    .pauseFor(500)
    .typeString('Renta de Carros')
    .pauseFor(500)
    .deleteChars(10)
    .start();