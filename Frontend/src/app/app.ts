import { Component, signal, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected readonly title = signal('BankaSimulasyonApiUI');

  ngOnInit() {
    history.pushState(null, '', location.href);
    window.onpopstate = () => {
      history.go(1);
    };
  }
}