import { useEffect, useState } from 'react'
import AuthPage from './components/AuthPage.jsx'
import Layout from './components/Layout.jsx'
import AfishaPage from './components/AfishaPage.jsx'
import MoviesPage from './components/MoviesPage.jsx'
import MoviePage from './components/MoviePage.jsx'
import SessionsAdminPage from './components/SessionsAdminPage.jsx'

const STORAGE_KEY = 'films-user'

const currentPath = () => {
  const path = window.location.hash.slice(1)
  return path && path !== '/' ? path : '/afisha'
}

function renderPage(path) {
  const [, section, id] = path.split('/')
  if (section === 'movies' && id) return <MoviePage movieId={Number(id)} />
  if (section === 'movies') return <MoviesPage />
  if (section === 'admin') return <SessionsAdminPage />
  return <AfishaPage />
}

export default function App() {
  const [user, setUser] = useState(() => JSON.parse(localStorage.getItem(STORAGE_KEY) ?? 'null'))
  const [path, setPath] = useState(currentPath)

  useEffect(() => {
    const onHashChange = () => setPath(currentPath())
    window.addEventListener('hashchange', onHashChange)
    return () => window.removeEventListener('hashchange', onHashChange)
  }, [])

  const handleLogin = (u) => {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(u))
    setUser(u)
  }

  const handleLogout = () => {
    localStorage.removeItem(STORAGE_KEY)
    setUser(null)
  }

  if (!user) return <AuthPage onLogin={handleLogin} />

  return (
    <Layout user={user} path={path} onLogout={handleLogout}>
      {renderPage(path)}
    </Layout>
  )
}
